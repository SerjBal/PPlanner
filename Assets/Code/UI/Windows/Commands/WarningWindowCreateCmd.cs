namespace SerjBal
{
    public class WarningWindowCreateCmd<T> : ICommand where T : IWindowPresenter, new()
    {
        private readonly IWindowsFactory _windowsFactory;
        private readonly IWindowPresenter _windowPresenter;

        public WarningWindowCreateCmd(IWindowPresenter windowPresenter, Services services)
        {
            _windowPresenter = windowPresenter;
            _windowsFactory = services.Single<IWindowsFactory>();
        }

        public void Execute(object param = null)
        {
            _windowsFactory.CreateWarningWindow<T>(_windowPresenter, (string)param);
        }
    }
}