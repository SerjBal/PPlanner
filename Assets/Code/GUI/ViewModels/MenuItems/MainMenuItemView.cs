using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SerjBal
{
    public class MainMenuItemView : MonoBehaviour, IMenuItemViewModel
    {
        [SerializeField] private Transform highScreenContainer;
        [SerializeField] private Transform lowScreenContainer;
        public CanvasGroup canvasGroup;
        private ItemViewModel _contentList;
        public Transform ViewTransform { get; }
        public Transform ContentContainer => lowScreenContainer;
        public string Key { get; set; }
        
        public void Initialize(ButtonConfigs configs, IAppFactory factory)
        {
            throw new System.NotImplementedException();
        }

        public void OnExpand()
        {
            throw new System.NotImplementedException();
        }

        public void OnCollapsed()
        {
            throw new System.NotImplementedException();
        }

        public void ChangeKey(string newKey)
        {
            throw new NotImplementedException();
        }
    }
}