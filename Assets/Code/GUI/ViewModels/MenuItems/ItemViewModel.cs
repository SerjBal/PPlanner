using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class ItemViewModel : MonoBehaviour
    {
        public IMenuItemViewModel Parent { get; set; }
        public string Key { get; set; }
        public Transform ContentContainer => contentContainer;
        public Dictionary<string, IMenuItemViewModel> ContentList { get; set; }
        public Transform ViewTransform => transform;
        
        [SerializeField] private ButtonSwipeController buttonsController;
        [SerializeField] private ExpandAnimator animator;
        [SerializeField] private Button newItemButton;
        [SerializeField] private Button editButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Transform contentContainer;
        protected Action OnAddNewItem{ get; set; }
        protected Action OnEditItem{ get; set; }
        

        public void Initialize(ButtonConfigs configs, IMenuItemViewModel menuItemViewModel)
        {
            Binds();
            ContentList = new Dictionary<string, IMenuItemViewModel>();
            Parent = menuItemViewModel;
            contentContainer.gameObject.SetActive(false);
            
            animator.Initialize(configs.expandAnimationCurve);
            buttonsController.Initialize(configs);
        }

        private void Binds()
        {
            newItemButton.onClick.AddListener(AddNewItem);
            editButton.onClick.AddListener(Edit);
            removeButton.onClick.AddListener(Remove);
            animator.onExpandEvent = OnExpand;
            animator.onCollapsedEvent = OnCollapsed;
            buttonsController.onSelectedEvent = OnSelected;
        }

        public void ChangeKey(string newKey)
        {
            nameText.text = newKey;
            Key = newKey;
        }

        public void AddNewItem() => OnAddNewItem?.Invoke();

        public virtual void OnExpand() => contentContainer.gameObject.SetActive(true);

        public virtual void OnCollapsed() => contentContainer.gameObject.SetActive(false);

        public void OnSelected() => animator.AnimationPlay();

        private void Remove() => Destroy(gameObject);
        
        private void Edit() => OnEditItem?.Invoke();
    }
}