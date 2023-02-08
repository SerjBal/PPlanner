using System;

namespace SerjBal
{
    public class TimeMenuItem : ItemViewModel, IMenuItem
    {
        private Action _onExpand;
        private string _textKey;

        public void Initialize(ButtonConfigs configs, IAppFactory factory)
        {
            itemType = MenuItemType.Time;
            _textKey = GetTextKey();
            OnAddNewItem = () => factory.CreateTextEditor(this, _textKey);
            OnEditItem = () => factory.CreateEditTimeWindow(this);
            base.Initialize(configs);
        }

        private string GetTextKey()
        {
            var data = new Services().Single<IDataProvider>();
            return data.GetOrCreateTimeData(Parent.Key, Key).Content[0].Key;
        }

        public override void OnExpand()
        {
            base.OnExpand();
            OnAddNewItem.Invoke();
        }

        public override void OnCollapsed()
        {
            base.OnExpand();
            Destroy(ContentContainer.GetChild(0).gameObject);
        }
    }
}
