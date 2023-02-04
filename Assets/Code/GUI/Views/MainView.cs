using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SerjBal
{
    public class MainView : MonoBehaviour, IViewModel
    {
        public Transform HighScreenContainer;
        public Transform LowScreenContainer;
        public CanvasGroup canvasGroup;
        private ItemViewModel _contentList;
        public Action OnAddNewItem { get; set; }
        public Transform ViewTransform { get; }
        public Transform ContentContainer
        {
            get { return LowScreenContainer; }
            set { }
        }
        public string Key { get; set; }
        
        
        public void AddToList(IViewModel prefab)
        {
            prefab.ViewTransform.SetParent(LowScreenContainer);
            _contentList.AddToList(prefab);
        }

        public void Remove()
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

        public void Initialize(ButtonConfigs configs, IAppFactory factory)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeKey(string newKey)
        {
            throw new NotImplementedException();
        }
    }
}