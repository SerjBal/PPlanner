
using System.Collections.Generic;
using SerjBal.Code.Sources;
using UnityEngine;

namespace SerjBal
{
    public class DateMenuItem : ItemViewModel
    {
        private IWindowsFactory _windowsFactory;

        public override void Initialize(ButtonConfigs configs)
        {
            base.Initialize(configs);
            _windowsFactory = _services.Single<IWindowsFactory>();
            itemType = MenuItemType.Date;
            onAddNewItem += () => _windowsFactory.CreateNewChannelWindow(this);
            onEditItem += () => _windowsFactory.CreateEditDateWindow(this);
        }

        public override void OnExpandStart()
        {
            ShowContent();
            base.OnExpandStart();
        }

        public override void OnExpandFinish() => _services.Single<IGUIModelView>().EnableCalendar(false);
        public override void OnCollapseStart()
        {
            _services.Single<IGUIModelView>().EnableCalendar(true);
            base.OnCollapseStart();
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
