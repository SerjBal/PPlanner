namespace SerjBal
{
    public class DateMenuItem : ItemViewModel
    {
        private IWindowsFactory _windowsFactory;

        public override void Initialize(ButtonConfigs configs)
        {
            base.Initialize(configs);
            _windowsFactory = _services.Single<IWindowsFactory>();
            itemType = MenuItemType.Date;
            onAddAction += () => _windowsFactory.CreateNewChannelWindow(this);
            onEditAction += () => _windowsFactory.CreateEditDateWindow(this);
        }
    }
}
