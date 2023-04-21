namespace SerjBal
{
    public class SearchCollapseEndCmd : ICommand
    {
        private readonly SplitButtonPresenter _presenter;

        public SearchCollapseEndCmd(SplitButtonPresenter presenter)
        {
            _presenter = presenter;
        }

        public void Execute(object param = null)
        {
            _presenter.ContentContainer.Clear();
            _presenter.ContentContainer.gameObject.SetActive(false);
            _presenter.IsOverrideSorting = false;
        }
    }
}