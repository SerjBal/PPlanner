namespace SerjBal
{
    internal class ConfirmCmd : ICommand
    {
        private readonly IWindowPresenter _editPresenter;
        private readonly string _path;
        private readonly IWindowPresenter _warningPresenter;

        public ConfirmCmd(IWindowPresenter warningPresenter, IWindowPresenter windowPresenter, string path)
        {
            _warningPresenter = warningPresenter;
            _editPresenter = windowPresenter;
            _path = path;
        }

        public void Execute(object param = null)
        {
            _editPresenter.AcceptCmd.Execute(_path);
            _warningPresenter.OnClose.Invoke();
        }
    }
}