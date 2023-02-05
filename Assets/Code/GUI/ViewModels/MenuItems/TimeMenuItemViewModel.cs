using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerjBal
{
    public class TimeMenuItemViewModel : ItemViewModel, IMenuItemViewModel
    {
        public void Initialize(ButtonConfigs configs, IAppFactory factory)
        {
            OnAddNewItem = () => factory.CreateNewTimeWindow(this);
            OnEditItem =  () => factory.CreateEditTimeWindow(this);
            base.Initialize(configs, this);
        }
    }
}
