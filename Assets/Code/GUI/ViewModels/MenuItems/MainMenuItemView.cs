using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SerjBal
{
    public class MainMenuItemView : MonoBehaviour, IMenuItemViewModel
    {
        //[SerializeField] private Transform highScreenContainer;
        public RectTransform lowScreenContainer;
        public CanvasGroup canvasGroup;
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