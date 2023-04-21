namespace SerjBal
{
    public class ButtonEditCmd<T> : ICommand where T : IWindowPresenter, new()
    {
        private readonly IHierarchical _buttonViewModel;
        private readonly Services _services;

        public ButtonEditCmd(IHierarchical buttonViewModel, Services services)
        {
            _buttonViewModel = buttonViewModel;
            _services = services;
        }

        public void Execute(object param = null)
        {
            _services.Single<IWindowsFactory>().CreateEditWindow<T>(_buttonViewModel, Const.ItemNamingWindowPath);
        }
    }
}