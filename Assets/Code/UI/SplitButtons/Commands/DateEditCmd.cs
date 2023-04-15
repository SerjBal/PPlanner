namespace SerjBal
{
    public class DateEditCmd : ICommand
    {
        private readonly IHierarchical _buttonViewModel;
        private readonly Services _services;

        public DateEditCmd(IHierarchical buttonViewModel, Services services)
        {
            _buttonViewModel = buttonViewModel;
            _services = services;
        }

        public void Execute(object param = null)
        {
            _services.Single<IWindowsFactory>().CreateEditWindow<EditDateWindow>(_buttonViewModel, Const.EditDateWindowPath);
        }
    }
}