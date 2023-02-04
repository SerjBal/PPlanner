using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class ItemViewModel : MonoBehaviour, IViewModel
    {
        [SerializeField] private ButtonSwipeController buttonsController;
        [SerializeField] private Button newItemButton;
        [SerializeField] private Button editButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Transform contentContainer;

        public Transform ContentContainer => contentContainer;

        private List<IViewModel> _contentList;
        private CanvasGroup _canvasGroup;
        private ButtonConfigs _configs;
        public Action OnAddNewItem{ get; set; }
        public Action OnEditItem{ get; set; }
        public string Key { get; set; }

        public Transform ViewTransform => transform;

        public void Initialize(ButtonConfigs configs, IAppFactory factory)
        {
            //_canvasGroup = gui.GetCanvasGroup();
            _configs = configs;
            OnAddNewItem = () => factory.CreateNewChannelWindow(this);
            OnEditItem =  () => factory.CreateEditDateWindow(this);
            newItemButton.onClick.AddListener(AddNewItem);
            editButton.onClick.AddListener(Edit);
            removeButton.onClick.AddListener(Remove);
            buttonsController.Initialize(this, _configs);
        }

        public void ChangeKey(string newKey)
        {
            nameText.text = newKey;
            Key = newKey;
        }

        public void AddNewItem()
        {
            OnAddNewItem.Invoke();
            Save();
        }

        public void OnExpand()
        {
        }

        public void OnCollapsed()
        {
        }

        public void Lock(bool isTrue) => _canvasGroup.interactable = isTrue;

        public void AddToList(IViewModel prefab)
        {
            if (_contentList==null) _contentList = new List<IViewModel>();
            _contentList.Add(prefab);
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        public void Edit()
        {
            OnEditItem.Invoke();
        }
        
        public void Save()
        {
            
        }
    }
}