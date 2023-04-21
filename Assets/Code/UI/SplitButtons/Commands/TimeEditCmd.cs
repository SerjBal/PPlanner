
namespace SerjBal
{
    public class TimeEditCmd<T> : ICommand where T : IWindowPresenter, new()
    {
        private readonly IHierarchical _buttonViewModel;
        private readonly IWindowsFactory _factory;

        public TimeEditCmd(IHierarchical buttonViewModel, Services services)
        {
            _buttonViewModel = buttonViewModel;
            _factory = services.Single<IWindowsFactory>();
        }

        public void Execute(object param = null)
        {
            _factory.CreateEditWindow<T>(_buttonViewModel,Const.EditTimeWindowPath);
        }
    }
}