namespace SerjBal
{
    internal class ConfirmCmd : ICommand
    {
        private readonly IWindowViewModel _editViewModel;
        private readonly string _path;
        private readonly IWindowViewModel _warningViewModel;

        public ConfirmCmd(IWindowViewModel warningViewModel, IWindowViewModel windowViewModel, string path)
        {
            _warningViewModel = warningViewModel;
            _editViewModel = windowViewModel;
            _path = path;
        }

        public void Execute(object param = null)
        {
            _editViewModel.AcceptCmd.Execute(_path);
            _warningViewModel.OnClose.Invoke();
        }
    }
}