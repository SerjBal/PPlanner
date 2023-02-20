using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class ItemViewModel : MonoBehaviour, IMenuItem
    {
        public IMenuItem Parent { get; set; }
        public List<IMenuItem> Childs { get; set;}
        public string Key { get; set; }
        public Transform ContentContainer => contentContainer;
        public MenuItemType itemType { get; set; }
        public Action onAddNewItem{ get; set; }
        public Action onEditItem{ get; set; }
        public bool isSelected { get; set; }
        [SerializeField] private Canvas canvas;
        [SerializeField] private ButtonSwipeController buttonsController;
        [SerializeField] private MenuItemAnimator animator;
        [SerializeField] private Button editButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Transform contentContainer;
        protected Services _services;
        protected IMenuFactory _factory;
        protected IDataProvider _data;

        public virtual void Initialize(ButtonConfigs configs)
        {
            Binds();
            Childs = new List<IMenuItem>();
            _services = new Services();
            _factory = _services.Single<IMenuFactory>();
            _data = _services.Single<IDataProvider>();
            animator.Initialize(configs.expandAnimationCurve);
            buttonsController.Initialize(configs);
            contentContainer.gameObject.SetActive(false);
        }
        private void Binds()
        {
            editButton.onClick.AddListener(OnEditItem);
            removeButton.onClick.AddListener(Remove);
            animator.onExpandStartEvent = OnExpandStart;
            animator.onExpandFinishEvent = OnExpandFinish;
            animator.onCollapseStartEvent = OnCollapseStart;
            animator.onCollapseFinishEvent = OnCollapseFinish;
            buttonsController.onSelectedEvent = OnSelected;
        }

        public void SetKey(string key)
        {
            nameText.text = key;
            Key = key;
        }
        public void ChangeKey(string newKey)
        {
            _data.RenameKey(this.GetKeyPath(), newKey);
            SetKey(newKey);
        }
        public void OnEditItem() => onEditItem.Invoke();
        
        public virtual void OnExpandStart()
        {
            UpdateContent();
            contentContainer.gameObject.SetActive(true);
            canvas.overrideSorting = true;
            isSelected = true;
        }
        
        public virtual void OnExpandFinish() { }

        public virtual void OnCollapseStart()
        {
            if (isSelected)
            {
                foreach (IMenuItem child in Childs)child.Collapse();
            }
        }
        public virtual void OnCollapseFinish()
        {
            isSelected = false;
            Childs = new List<IMenuItem>();
            contentContainer.gameObject.SetActive(false);
            canvas.overrideSorting = false;
            foreach (Transform item in ContentContainer) {Destroy(item.gameObject);}
        }

        public void Collapse()
        {
            if (isSelected) animator?.PlayClose();
        }

        public void OnAddNewItem() => onAddNewItem?.Invoke();
        public void OnSelected() => animator.AnimationPlay();

        public virtual async void UpdateContent()
        {
            var keyPath = this.GetKeyPath();
            
            Childs.Clear();
            ContentContainer.Clear();
                
            ItemData itemData =  _data.GetOrCreateData(keyPath);
            for (int i = 0; i < itemData.Content.Count; i++)
            {
                Childs.Add(await _factory.CreateMenuItem(this, itemData.Content[i].Key));
            }
            
            var addButton = await _factory.CreateAddButton(ContentContainer);
            addButton.onClick.AddListener(OnAddNewItem);
            animator.onExpandFinishEvent?.Invoke();
        }

        public void Remove()
        {
            _services.Single<IDataProvider>().RemoveKey(this.GetKeyPath());
            if (Parent!=null)
            {
                Parent.UpdateContent();
            }
            else
            {
                Collapse();
            }
            _services.Single<ISaveLoad>().Save();
        }
    }
}