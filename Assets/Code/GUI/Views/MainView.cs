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
        public Transform ViewTransform { get; }
        public string Key { get; set; }
        
        public void Add(IViewModel prefab)
        {
            prefab.ViewTransform.SetParent(LowScreenContainer);
            _contentList.Add(prefab);
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

        public void Initialize(ButtonConfigs configs)
        {
            throw new System.NotImplementedException();
        }
    }
}