namespace SerjBal
{
    public class ChannelFormatCmd : ICommand
    {
        private readonly IWindowViewModel _viewModel;

        public ChannelFormatCmd(IWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.InputString = "New Channel";
        }

        public void Execute(object param = null)
        {
        }
    }
}