using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerjBal
{
    public class DateMenuItemViewModel : ItemViewModel, IMenuItemViewModel
    {
        public void Initialize(ButtonConfigs configs, IAppFactory factory)
        {
            OnAddNewItem = () => factory.CreateNewChannelWindow(this);
            OnEditItem =  () => factory.CreateEditDateWindow(this);
            base.Initialize(configs,  this);
        }
    }
}
