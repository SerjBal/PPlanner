
using System.Collections.Generic;
using SerjBal.Code.Sources;
using UnityEngine;

namespace SerjBal
{
    public class DateMenuItem : ItemViewModel
    {
        public override void Initialize(ButtonConfigs configs)
        {
            base.Initialize(configs);
            itemType = MenuItemType.Date;
            onAddNewItem += () => _factory.CreateNewChannelWindow(this);
            onEditItem += () => _factory.CreateEditDateWindow(this);
            animator.AnimationPlay();
        }

        public override async void OnExpand()
        {
            ShowContent();
            base.OnExpand();
        }

        public async void ShowContent()
        {
            ItemData dateData = _data.GetOrCreateDateData();
            if (dateData.Content!=null && dateData.Content.Count>0)
            {
                foreach (ItemData item in dateData.Content)
                {
                    Childs.Add(await _factory.CreateChannelItem(this, item.Key));
                }
            }
            var addButton = await _factory.CreateAddButton(ContentContainer);
            addButton.onClick.AddListener(OnAddNewItem);
        }
    }
}
