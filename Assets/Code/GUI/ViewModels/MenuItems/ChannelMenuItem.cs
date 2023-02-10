using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerjBal
{
    public class ChannelMenuItem : ItemViewModel
    {
        public override void Initialize(ButtonConfigs configs)
        {
            base.Initialize(configs);
            itemType = MenuItemType.Channel;
            onAddNewItem += () => _factory.CreateNewTimeWindow(this);
            onEditItem += () => _factory.CreateEditChannelWindow(this);
        }
        
        public override async void OnExpand()
        {
            ItemData channelData = _data.GetOrCreateChannelData(Key);
            if (channelData!=null && channelData.Content.Count > 0)
            {
                foreach (var item in channelData.Content)
                {
                    Childs.Add(await _factory.CreateTimeItem(this, item.Key));
                }
            }
            var addButton = await _factory.CreateAddButton(ContentContainer);
            addButton.onClick.AddListener(OnAddNewItem);
            base.OnExpand();
        }
    }
}
