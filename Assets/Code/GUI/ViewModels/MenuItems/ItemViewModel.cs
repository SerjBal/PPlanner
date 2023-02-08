using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class ItemViewModel : MonoBehaviour
    {
        public IMenuItem Parent { get; set; }
        public string Key { get; set; }
        public Transform ContentContainer => contentContainer;
        public Transform ViewTransform => transform;
        
        [SerializeField] private ButtonSwipeController buttonsController;
        [SerializeField] protected ExpandAnimator animator;
        [SerializeField] private Button editButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Transform contentContainer;
        protected Action OnAddNewItem{ get; set; }
        protected Action OnEditItem{ get; set; }
        

        public void Initialize(ButtonConfigs configs, IMenuItem menuItem)
        {
            Binds();
            Parent = menuItem;
            contentContainer.gameObject.SetActive(false);
            
            animator.Initialize(configs.expandAnimationCurve);
            buttonsController.Initialize(configs);
        }

        private void Binds()
        {
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

        public virtual void OnCollapsed()
        {
            contentContainer.gameObject.SetActive(false);
            foreach (Transform item in ContentContainer) {Destroy(item.gameObject);}
        }

        public void OnSelected() => animator.AnimationPlay();

        private void Remove() => Destroy(gameObject);
        
        private void Edit() => OnEditItem?.Invoke();
    }
}