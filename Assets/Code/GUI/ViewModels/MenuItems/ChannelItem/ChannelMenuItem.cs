
namespace SerjBal
{
    public class ChannelMenuItem : ItemViewModel
    {
        private IWindowsFactory _windowsFactory;
        public override void Initialize(ButtonConfigs configs)
        {
            base.Initialize(configs);
            _windowsFactory = _services.Single<IWindowsFactory>();
            itemType = MenuItemType.Channel;
            onAddAction += () => _windowsFactory.CreateNewTimeWindow(this);
            onEditAction += () => _windowsFactory.CreateEditChannelWindow(this);
        }
    }
}
