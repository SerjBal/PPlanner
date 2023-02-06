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
        [SerializeField] private ExpandAnimator animator;
        [SerializeField] private Button newItemButton;
        [SerializeField] private Button editButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Transform contentContainer;
        public Transform ContentContainer => contentContainer;

        public Dictionary<string, IMenuItemViewModel> ContentList { get; set; }
        private CanvasGroup _canvasGroup;
        public Action OnAddNewItem{ get; set; }
        public Action OnEditItem{ get; set; }
        public string Key { get; set; }

        public Transform ViewTransform => transform;

        public void Initialize(ButtonConfigs configs, IMenuItemViewModel menuItemViewModel)
        {
            Binds();
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

        public void OnExpand() => contentContainer.gameObject.SetActive(true);

        public void OnCollapsed() => contentContainer.gameObject.SetActive(false);

        public void OnSelected() => animator.AnimationPlay();

        public void Lock(bool isTrue) => _canvasGroup.interactable = isTrue;

        private void Remove() => Destroy(gameObject);
        private void Edit() => OnEditItem?.Invoke();
    }
}