namespace SerjBal
{
    public class ButtonCollapseStartCmd : ICommand
    {
        private readonly ButtonViewModel _viewModel;

        public ButtonCollapseStartCmd(ButtonViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Execute(object param = null)
        {
            if (_viewModel.IsSelected)
                foreach (ButtonViewModel child in _viewModel.ChildList)
                    if (child.IsSelected)
                        child.PushButton();
        }
    }
}