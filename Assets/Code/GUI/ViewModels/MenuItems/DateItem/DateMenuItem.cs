
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
        public override void OnExpandFinish() => _services.Single<IGUIModelView>().EnableCalendar(false);
        public override void OnCollapseStart()
        {
            _services.Single<IGUIModelView>().EnableCalendar(true);
            base.OnCollapseStart();
        }
    }
}
