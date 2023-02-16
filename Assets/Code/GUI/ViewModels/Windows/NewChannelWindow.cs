using System.Collections.Generic;
using SerjBal.Windows;
using UnityEngine;

namespace SerjBal
{
    public class NewChannelWindow : NewItemWindow, IWindow
    {
        public override void Initialize(IMenuItem menuItem)
        {
            InputField.text = "New Channel";
            base.Initialize(menuItem);
            onAccept += () => UpdateContent(menuItem);
        }

        private async void UpdateContent(IMenuItem menuItem)
        {
            var _factory = _services.Single<IMenuFactory>();
            foreach (Transform item in menuItem.ContentContainer) Destroy(item.gameObject);
            menuItem.Childs = new List<IMenuItem>();

            ItemData dateData = _services.Single<IDataProvider>().GetOrCreateDateData();
            if (dateData.Content!=null && dateData.Content.Count>0)
            {
                foreach (ItemData item in dateData.Content)
                {
                    menuItem.Childs.Add(await _factory.CreateChannelItem(menuItem, item.Key));
                }
            }
            var addButton = await _factory.CreateAddButton(menuItem.ContentContainer);
            addButton.onClick.AddListener(menuItem.OnAddNewItem);
        }
    }
}