using System;
using SerjBal.Code.Sources;

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
            canvas.sortingOrder = Const.SelectedItemSortingOrder+2;
            itemType = MenuItemType.Time;
            _textKey = GetTextKey();
            onAddNewItem += () => _factory.CreateTextEditor(this, _textKey);
            onEditItem += () => _windowsFactory.CreateEditTimeWindow(this);
        }

        private string GetTextKey()
        {
            return _data.GetOrCreateTimeData(Parent.Key, Key).Content[0].Key;
        }

        public override void OnExpand()
        {
            base.OnExpand();
            OnAddNewItem();
        }

        public override void OnCollapsed()
        {
            base.OnExpand();
            Destroy(ContentContainer.GetChild(0).gameObject);
        }
    }
}
