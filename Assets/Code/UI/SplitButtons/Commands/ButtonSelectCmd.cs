namespace SerjBal
{
    public class ButtonSelectCmd : ICommand
    {
        private readonly ButtonViewModel _viewModel;

        public ButtonSelectCmd(ButtonViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Execute(object param)
        {
            _viewModel.IsSelected = !_viewModel.IsSelected;
        }
    }
}