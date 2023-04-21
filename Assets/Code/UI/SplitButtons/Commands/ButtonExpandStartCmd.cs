namespace SerjBal
{
    public class ButtonExpandStartCmd : ICommand
    {
        private readonly SplitButtonPresenter _presenter;

        public ButtonExpandStartCmd(SplitButtonPresenter presenter)
        {
            _presenter = presenter;
        }

        public void Execute(object param = null)
        {
            _presenter.ContentUpdateCommand?.Execute();
            _presenter.ContentContainer.gameObject.SetActive(true);
            _presenter.IsOverrideSorting = true;
        }
    }
}