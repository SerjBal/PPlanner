namespace SerjBal
{
    public class WarningWindowCreateCmd<T> : ICommand where T : IWindowViewModel, new()
    {
        private readonly IWindowsFactory _windowsFactory;
        private readonly IWindowViewModel _windowViewModel;

        public WarningWindowCreateCmd(IWindowViewModel windowViewModel, Services services)
        {
            _windowViewModel = windowViewModel;
            _windowsFactory = services.Single<IWindowsFactory>();
        }

        public void Execute(object param = null)
        {
            _windowsFactory.CreateWarningWindow<T>(_windowViewModel, (string)param);
        }
    }
}