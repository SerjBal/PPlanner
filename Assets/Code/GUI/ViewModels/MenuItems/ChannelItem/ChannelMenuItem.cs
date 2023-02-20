using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SerjBal.Code.Sources;
using UnityEngine;

namespace SerjBal
{
    public class ChannelMenuItem : ItemViewModel
    {
        private IWindowsFactory _windowsFactory;
        public override void Initialize(ButtonConfigs configs)
        {
            base.Initialize(configs);
            _windowsFactory = _services.Single<IWindowsFactory>();
            itemType = MenuItemType.Channel;
            onAddNewItem += () => _windowsFactory.CreateNewTimeWindow(this);
            onEditItem += () => _windowsFactory.CreateEditChannelWindow(this);
        }
    }
}
