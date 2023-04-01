namespace SerjBal
{
    public class TextEditorAddCmd : ICommand
    {
        private readonly IMenuFactory _factory;
        private readonly IHierarchical _viewModel;

        public TextEditorAddCmd(IHierarchical viewModel, Services services)
        {
            _viewModel = viewModel;
            _factory = services.Single<IMenuFactory>();
        }

        public void Execute(object param = null)
        {
            _viewModel.ContentContainer.Clear();
            _factory.CreateTextEditor(_viewModel, _viewModel.Path);
        }
    }
}