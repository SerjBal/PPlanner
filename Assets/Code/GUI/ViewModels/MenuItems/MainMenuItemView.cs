using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SerjBal
{
    public class MainMenuItemView : MonoBehaviour
    {
        //[SerializeField] private Transform highScreenContainer;
        public RectTransform lowScreenContainer;
        public CanvasGroup canvasGroup;
        public IMenuItemViewModel Parent { get; set; }
        public Transform ViewTransform { get; }
        public Dictionary<string, IMenuItemViewModel> ContentList { get; set; }
        public Transform ContentContainer => lowScreenContainer;
        public string Key { get; set; }
        
        public void Initialize(ButtonConfigs configs, IAppFactory factory){ }
        public void OnExpand() { }
        public void OnCollapsed(){ }
        public void ChangeKey(string newKey){ }
    }
}