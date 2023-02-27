using System;
using SerjBal.Code.Sources;
using UnityEngine;

namespace SerjBal
{
    public class TimeMenuItem : ItemViewModel
    {
        private Action _onExpand;
        private string _textKey;
        private IWindowsFactory _windowsFactory;
        private bool _exists;

        public override void Initialize(ButtonConfigs configs)
        {
            base.Initialize(configs);
            _windowsFactory = _services.Single<IWindowsFactory>();
            itemType = MenuItemType.Time;
            _textKey = GetTextKey();
            onEditAction += () => _windowsFactory.CreateEditTimeWindow(this);
        }

        private string GetTextKey()
        {
            _exists = _data.HasKey(this.GetKeyPath());
            var key = _data.GetOrCreateData(this.GetKeyPath()).Content[0].Key;
            if (_exists) _services.Single<ISaveLoad>().Save();
            return key;
        }

        public override void OnExpandStart()
        {
            base.OnExpandStart();
            OnAddNewItem();
        }
        
        public override async void UpdateContent()
        {
            await _factory.CreateTextEditor(this, _textKey);
        }
    }
}
