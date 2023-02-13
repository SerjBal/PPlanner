
using SerjBal.Windows;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerjBal
{
    public class NewPostWindow : NewItemWindow, IWindow
    {
        public override void Initialize(IMenuItem menuItem)
        {
            base.Initialize(menuItem);
            InputField.text = "0:0";
            onAccept += () => UpdateContent(menuItem);
        }

        private async void UpdateContent(IMenuItem menuItem)
        {
            var _factory = _services.Single<IMenuFactory>();
            foreach (Transform item in menuItem.ContentContainer) Destroy(item.gameObject);

            ItemData channelData = _services.Single<IDataProvider>().GetOrCreateChannelData(menuItem.Key);
            if (channelData != null && channelData.Content.Count > 0)
            {
                foreach (var item in channelData.Content) await _factory.CreateTimeItem(menuItem, item.Key);
            }

            var addButton = await _factory.CreateAddButton(menuItem.ContentContainer);
            addButton.onClick.AddListener(menuItem.OnAddNewItem);
        }
    }
}

