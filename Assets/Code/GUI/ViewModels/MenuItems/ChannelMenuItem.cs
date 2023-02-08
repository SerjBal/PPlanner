using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerjBal
{
    public class ChannelMenuItem : ItemViewModel, IMenuItem
    {
        private IAppFactory _factory;
        private IDataProvider _data;

        public void Initialize(ButtonConfigs configs, IAppFactory factory)
        {
            _factory = factory;
            _data = new Services().Single<IDataProvider>();
            OnAddNewItem = () => factory.CreateNewTimeWindow(this);
            OnEditItem =  () => factory.CreateEditChannelWindow(this);
            base.Initialize(configs, this);
        }
        
        public override async void OnExpand()
        {
            IData channelData = _data.GetOrCreateChannelData(Key);
            if (channelData!=null && channelData.Content.Count > 0)
            {
                foreach (string key in channelData.Content.Keys)
                {
                    await _factory.CreateTimeItem(this, key);
                }
            }
            var addButton = await _factory.CreateAddButton(ContentContainer);
            addButton.onClick.AddListener(AddNewItem);
            base.OnExpand();
        }
    }
}
