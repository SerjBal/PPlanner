namespace SerjBal
{
    public class NameFormatCmd : ICommand
    {
        private readonly IWindowPresenter _presenter;

        public NameFormatCmd(IWindowPresenter presenter)
        {
            _presenter = presenter;
            _presenter.InputString = "New Channel";
        }

        public void Execute(object param = null)
        {
            if (param != null)
            {
                var value = (string)param;
                if ( value.Length > 15) 
                    value = value.Substring(0, 15);
                _presenter.InputString = value;
            }
        }
    }
}