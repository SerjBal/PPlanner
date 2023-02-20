using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace SerjBal
{
    public interface IMenuItem
    {
        IMenuItem Parent { get; set; }
        List<IMenuItem> Childs { get; set;}
        Transform ContentContainer { get;}
        string Key { get; set; }
        MenuItemType itemType { get; set; }
        bool isSelected { get; set; }
        void Initialize(ButtonConfigs configs);
        void SetKey(string key);
        void ChangeKey(string newKey);
        void UpdateContent();
        void Collapse();
        void OnAddNewItem();
        void Remove();
    }
}