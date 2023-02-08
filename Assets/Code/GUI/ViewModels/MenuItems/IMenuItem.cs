using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SerjBal
{
    public interface IMenuItem
    {
        public IMenuItem Parent { get; set; }
        public Transform ViewTransform { get; }
        Transform ContentContainer { get;}
        public string Key { get; set; }
        MenuItemType itemType { get; set; }
        void OnExpand();
        void OnCollapsed();
        void Initialize(ButtonConfigs configs, IAppFactory factory);
        void ChangeKey(string newKey);
    }
}