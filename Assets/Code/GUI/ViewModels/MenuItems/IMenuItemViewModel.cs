using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SerjBal
{
    public interface IMenuItemViewModel
    {
        public Transform ViewTransform { get; }
        public Dictionary<string, IMenuItemViewModel> ContentList { get; set; }
        Transform ContentContainer { get;}
        public string Key { get; set; }
        void OnExpand();
        void OnCollapsed();
        void Initialize(ButtonConfigs configs, IAppFactory factory);
        void ChangeKey(string newKey);
    }
}