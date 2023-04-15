using System.IO;

namespace SerjBal
{
    public class ButtonCheckNameCmd : ICommand
    {
        private readonly string _parentPath;
        private readonly IWindowViewModel _viewModel;

        public ButtonCheckNameCmd(IWindowViewModel viewModel, string parentPath)
        {
            _viewModel = viewModel;
            _parentPath = parentPath;
        }

        public virtual void Execute(object param = null)
        {
            var keyData = GetDateName();
            var keyPath = GetPath(keyData);

            Check(keyPath, keyData);
        }
        
        private protected virtual string GetDateName() => _viewModel.InputString;

        private string GetPath(string keyData)
        {
            var keyPath = _parentPath == null
                ? Path.Combine(Const.DataPath, keyData)
                : Path.Combine(_parentPath, keyData);
            return keyPath;
        }

        private void Check(string keyPath, string keyData)
        {
            if (Directory.Exists(keyPath))
                _viewModel.СonfirmCmd.Execute(keyData);
            else
                _viewModel.AcceptCmd.Execute(keyData);
        }
    }
}