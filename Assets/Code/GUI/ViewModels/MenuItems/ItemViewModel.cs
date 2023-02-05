using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class ItemViewModel : MonoBehaviour
    {
        [SerializeField] private ButtonSwipeController buttonsController;
        [SerializeField] private Button newItemButton;
        [SerializeField] private Button editButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Transform contentContainer;
        private IMenuItemViewModel _menuItemViewModel;

        public Transform ContentContainer => contentContainer;

        private List<IMenuItemViewModel> _contentList;
        private CanvasGroup _canvasGroup;
        private ButtonConfigs _configs;
        public Action OnAddNewItem{ get; set; }
        public Action OnEditItem{ get; set; }
        public string Key { get; set; }

        public Transform ViewTransform => transform;

        public void Initialize(ButtonConfigs configs, IMenuItemViewModel menuItemViewModel)
        {
            _configs = configs;
            _menuItemViewModel = menuItemViewModel;
            newItemButton.onClick.AddListener(AddNewItem);
            editButton.onClick.AddListener(Edit);
            removeButton.onClick.AddListener(Remove);
            buttonsController.Initialize(_menuItemViewModel, _configs);
        }

        public void ChangeKey(string newKey)
        {
            nameText.text = newKey;
            Key = newKey;
            Save();
        }

        public void AddNewItem()
        {
            OnAddNewItem?.Invoke();
            Save();
        }

        public void OnExpand()
        {
        }

        public void OnCollapsed()
        {
        }

        public void Lock(bool isTrue) => _canvasGroup.interactable = isTrue;

        private void Remove()
        {
            Destroy(gameObject);
        }

        private void Edit()
        {
            OnEditItem?.Invoke();
        }
        
        private void Save()
        {
            
        }
    }
}