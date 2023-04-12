namespace SerjBal
{
    public class NameFormatCmd : ICommand
    {
        private readonly IWindowViewModel _viewModel;

        public NameFormatCmd(IWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.InputString = "New Channel";
        }

        public void Execute(object param = null)
        {
            if (param != null)
            {
                var value = (string)param;
                if ( value.Length > 15) 
                    value = value.Substring(0, 15);
                _viewModel.InputString = value;
            }
        }
    }
}