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
        public string Key { get;  set; }
        public MenuItemType itemType { get; set; }
        public Action onAddAction{ get; set; }
        public Action onEditAction{ get; set; }
        public Transform ContentContainer => contentContainer;
        
        [SerializeField] protected Canvas canvas;
        [SerializeField] private ButtonSwipeController buttonsController;
        [SerializeField] private Button editButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] protected Transform contentContainer;
        [SerializeField] protected MenuItemAnimator animator;
        protected Services _services;
        protected IMenuFactory _factory;
        protected IDataProvider _data;
        private bool _isSelected;

        public virtual void Initialize(ButtonConfigs configs)
        {
            Bind();
            _isSelected = false;
            _services = new Services();
            _factory = _services.Single<IMenuFactory>();
            _data = _services.Single<IDataProvider>();
            Childs = new List<IMenuItem>();
            animator.Initialize(configs.expandAnimationCurve);
            contentContainer.gameObject.SetActive(false);
            buttonsController?.Initialize(configs);
        }
        private void Bind()
        {
            animator.onExpandStartEvent = OnExpandStart;
            animator.onExpandFinishEvent = OnExpandFinish;
            animator.onCollapseStartEvent = OnCollapseStart;
            animator.onCollapseFinishEvent = OnCollapseFinish;
            if (buttonsController!=null)
            {
                editButton.onClick.AddListener(OnEdit);
                removeButton.onClick.AddListener(Remove);
                buttonsController.onSelectedEvent = OnSelected;
            }
        }

        public void SetName(string key)
        {
            nameText.text = key;
            Key = key;
        }
        
        public void ShowContent()
        {
            if (!_isSelected)
            {
                _isSelected = true;
                animator?.PlayOpen();
            }
        }
        public void HideContent()
        {
            if (_isSelected)
            {
                _isSelected = false;
                animator?.PlayClose();
            }
        }
        
        public void OnSelected()
        {
            if (!_isSelected)
                {ShowContent();}
            else
                HideContent();
        }
        public void OnEdit() => onEditAction.Invoke();
        
        public virtual void OnExpandStart()
        {
            UpdateContent();
            contentContainer.gameObject.SetActive(true);
            canvas.overrideSorting = true;
        }
        
        public virtual void OnExpandFinish() { }

        public virtual void OnCollapseStart()
        {
            if (_isSelected)
            {
                foreach (IMenuItem child in Childs) child.HideContent();
            }
        }
        public virtual void OnCollapseFinish()
        {
            Childs = new List<IMenuItem>();
            contentContainer.gameObject.SetActive(false);
            canvas.overrideSorting = false;
            foreach (Transform item in ContentContainer) {Destroy(item.gameObject);}
        }

        public void OnAddNewItem() => onAddAction?.Invoke();

        public virtual async void UpdateContent()
        {
            Childs.Clear();
            ContentContainer.Clear();
                
            ItemData itemData =  _data.GetOrCreateData(this.GetKeyPath());
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
            _services.Single<ISaveLoad>().Save();
            if (Parent != null)
                Parent.UpdateContent();
            else
                HideContent();
        }
    }
}