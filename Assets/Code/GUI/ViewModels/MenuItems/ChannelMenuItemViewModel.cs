using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerjBal
{
    public class ChannelMenuItemViewModel : ItemViewModel, IMenuItemViewModel
    {
        public void Initialize(ButtonConfigs configs, IAppFactory factory)
        {
            OnAddNewItem = () => factory.CreateNewTimeWindow(this);
            OnEditItem =  () => factory.CreateEditChannelWindow(this);
            base.Initialize(configs, this);
        }
    }
}
