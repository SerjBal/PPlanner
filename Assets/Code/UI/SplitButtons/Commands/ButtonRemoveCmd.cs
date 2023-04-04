namespace SerjBal
{
    public class ButtonRemoveCmd : ICommand
    {
        private readonly IDataProvider _data;
        private readonly ICommand _selectCommand;
        private readonly Services _services;
        private readonly ButtonViewModel _viewModel;

        public ButtonRemoveCmd(IHierarchical viewModel, Services services)
        {
            _viewModel = viewModel as ButtonViewModel;
            _data = services.Single<IDataProvider>();
        }

        public void Execute(object param = null)
        {
            _data.DeleteDirectory(_viewModel.Path);

            if (_viewModel.Parent != null)
                ((ButtonViewModel)_viewModel.Parent).ContentUpdateCommand.Execute();
            else if (_viewModel.IsSelected)
                _viewModel.PushButton();
        }
    }
}