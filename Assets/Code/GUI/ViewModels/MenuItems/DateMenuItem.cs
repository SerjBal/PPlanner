
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
            _factory = factory;
            _data = new Services().Single<IDataProvider>();
            OnAddNewItem = () => factory.CreateNewChannelWindow(this);
            OnEditItem = () => factory.CreateEditDateWindow(this);
            base.Initialize(configs, this);
            animator.AnimationPlay();
        }

        public override async void OnExpand()
        {
            IData dateData = _data.GetDateData();
            if (dateData.Content!=null && dateData.Content.Count>0)
            {
                foreach (string key in dateData.Content.Keys)
                {
                    await _factory.CreateChannelItem(this, key);
                }
            }

            var addButton = await _factory.CreateAddButton(ContentContainer);
            addButton.onClick.AddListener(AddNewItem);
            base.OnExpand();
        }
    }
}
