namespace SerjBal
{
    public class ButtonCollapseStartCmd : ICommand
    {
        private readonly SplitButtonPresenter _presenter;

        public ButtonCollapseStartCmd(SplitButtonPresenter presenter)
        {
            _presenter = presenter;
        }

        public void Execute(object param = null)
        {
            if (_presenter.IsSelected)
                foreach (SplitButtonPresenter child in _presenter.ChildList)
                    if (child.IsSelected)
                        child.PushButton();
        }
    }
}