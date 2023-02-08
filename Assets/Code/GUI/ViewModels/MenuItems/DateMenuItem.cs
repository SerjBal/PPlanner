
using System.Collections.Generic;
using SerjBal.Code.Sources;
using UnityEngine;

namespace SerjBal
{
    public class DateMenuItem : ItemViewModel, IMenuItem
    {
        private IAppFactory _factory;
        private IDataProvider _data;
        public void Initialize(ButtonConfigs configs, IAppFactory factory)
        {
            itemType = MenuItemType.Date;
            _factory = factory;
            _data = new Services().Single<IDataProvider>();
            OnAddNewItem = () => factory.CreateNewChannelWindow(this);
            OnEditItem = () => factory.CreateEditDateWindow(this);
            base.Initialize(configs);
            animator.AnimationPlay();
        }

        public override async void OnExpand()
        {
            ItemData dateData = _data.GetOrCreateDateData();
            if (dateData.Content!=null && dateData.Content.Count>0)
            {
                foreach (ItemData item in dateData.Content)
                {
                    await _factory.CreateChannelItem(this, item.Key);
                }
            }
            var addButton = await _factory.CreateAddButton(ContentContainer);
            addButton.onClick.AddListener(AddNewItem);
            base.OnExpand();
        }
    }
}
