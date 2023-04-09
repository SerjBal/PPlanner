namespace SerjBal
{
    public class TimeEditCmd<T> : ICommand where T : IWindowViewModel, new()
    {
        private readonly IHierarchical _buttonViewModel;
        private readonly Services _services;

        public TimeEditCmd(IHierarchical buttonViewModel, Services services)
        {
            _buttonViewModel = buttonViewModel;
            _services = services;
        }

        public void Execute(object param = null)
        {
            _services.Single<IWindowsFactory>().CreateEditWindow<T>(_buttonViewModel, Const.EditTimeWindow);
        }
    }
}