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

        public override void Initialize(ButtonConfigs configs)
        {
            base.Initialize(configs);
            _windowsFactory = _services.Single<IWindowsFactory>();
            itemType = MenuItemType.Time;
            _textKey = GetTextKey();
            onAddNewItem += () => _factory.CreateTextEditor(this, _textKey);
            onEditItem += () => _windowsFactory.CreateEditTimeWindow(this);
        }

        private string GetTextKey() => _data.GetOrCreateData(this.GetKeyPath()).Content[0].Key;

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
