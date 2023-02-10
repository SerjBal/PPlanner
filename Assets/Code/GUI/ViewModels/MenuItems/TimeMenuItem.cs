using System;

namespace SerjBal
{
    public class TimeMenuItem : ItemViewModel
    {
        private Action _onExpand;
        private string _textKey;

        public override void Initialize(ButtonConfigs configs)
        {
            base.Initialize(configs);
            itemType = MenuItemType.Time;
            _textKey = GetTextKey();
            onAddNewItem += () => _factory.CreateTextEditor(this, _textKey);
            onEditItem += () => _factory.CreateEditTimeWindow(this);
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
