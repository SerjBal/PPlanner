using System;

namespace SerjBal
{
    public class TimeMenuItemViewModel : ItemViewModel, IMenuItemViewModel
    {
        private Action _onExpand;
        private string _textKey;

        public void Initialize(ButtonConfigs configs, IAppFactory factory)
        {
            _textKey = GetTextKey();
            OnAddNewItem = () => factory.CreateTextEditor(this, _textKey);
            OnEditItem = () => factory.CreateEditTimeWindow(this);
            base.Initialize(configs, this);
        }

        private string GetTextKey()
        {
            var data = new Services().Single<IDataProvider>();
            return data.GetTimeData(Parent.Key, Key).TextKey;
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
