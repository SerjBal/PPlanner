using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SerjBal
{
    public interface IViewModel
    {
        public Action OnAddNewItem { get; set; }
        public Transform ViewTransform { get; }

        Transform ContentContainer { get;}
        public string Key { get; set; }
        void Remove();
        void OnExpand();
        void OnCollapsed();
        void Initialize(ButtonConfigs configs, IAppFactory factory);
        void ChangeKey(string newKey);
    }
}