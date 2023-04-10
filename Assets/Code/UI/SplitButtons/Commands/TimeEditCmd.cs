using SerjBal.Indication;

namespace SerjBal
{
    public class TimeEditCmd<T> : ICommand where T : IWindowViewModel, new()
    {
        private readonly IHierarchical _buttonViewModel;
        private readonly IAssetsProvider _assets;
        private readonly Services _services;

        public TimeEditCmd(IHierarchical buttonViewModel, Services services)
        {
            _buttonViewModel = buttonViewModel;
            _services = services;
            _assets = services.Single<IAssetsProvider>();
        }

        public async void Execute(object param = null)
        {
            var viewModel = new T() as EditWindow;
            viewModel?.Initialize(_buttonViewModel, _services);
            var view = await _assets.Instantiate<TimeWindowView>(Const.EditTimeWindow, null);
            view.Initialize(viewModel);
        }
    }
}