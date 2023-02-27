using UnityEngine;

namespace SerjBal
{
    public interface IMenuItem
    {
        IMenuItem Parent { get; set; }
        Transform ContentContainer { get;}
        string Key { get; }
        MenuItemType itemType { get; set; }
        void Initialize(ButtonConfigs configs);
        void SetName(string key);
        void UpdateContent();
        void HideContent();
        void Remove();
    }
}