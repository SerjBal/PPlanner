namespace SerjBal
{
    public class ButtonRemoveCmd : ICommand
    {
        private readonly IDataProvider _data;
        private readonly ICommand _selectCommand;
        private readonly Services _services;
        private readonly SplitButtonPresenter _presenter;

        public ButtonRemoveCmd(IHierarchical viewModel, Services services)
        {
            _presenter = viewModel as SplitButtonPresenter;
            _data = services.Single<IDataProvider>();
        }

        public void Execute(object param = null)
        {
            _data.DeleteDirectory(_presenter.Path);

            if (_presenter.Parent != null)
                (_presenter.Parent as SplitButtonPresenter)?.ContentUpdateCommand?.Execute();
            else if (_presenter.IsSelected)
                _presenter.PushButton();
        }
    }
}