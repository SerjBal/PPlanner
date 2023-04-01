using System.IO;

namespace SerjBal
{
    public class ButtonCheckCmd : ICommand
    {
        private readonly string _parentPath;
        private readonly IWindowViewModel _viewModel;

        public ButtonCheckCmd(IWindowViewModel viewModel, string parentPath)
        {
            _viewModel = viewModel;
            _parentPath = parentPath;
        }

        public void Execute(object param = null)
        {
            var keyData = _viewModel.InputString;
            var keyPath = _parentPath == null
                ? Path.Combine(Const.DataPath, keyData)
                : Path.Combine(_parentPath, keyData);

            if (Directory.Exists(keyPath))
                _viewModel.СonfirmCmd.Execute(keyData);
            else
                _viewModel.AcceptCmd.Execute(keyData);
        }
    }
}