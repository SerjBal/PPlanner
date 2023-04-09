namespace SerjBal
{
    public class SearchCollapseEndCmd : ICommand
    {
        private readonly ButtonViewModel _viewModel;

        public SearchCollapseEndCmd(ButtonViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Execute(object param = null)
        {
            _viewModel.ContentContainer.Clear();
            _viewModel.ContentContainer.gameObject.SetActive(false);
            _viewModel.IsOverrideSorting = false;
        }
    }
}