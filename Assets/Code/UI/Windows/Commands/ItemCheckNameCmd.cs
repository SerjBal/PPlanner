using System.IO;

namespace SerjBal
{
    public class ItemCheckNameCmd : ICommand
    {
        private readonly string _parentPath;
        private readonly IWindowPresenter _presenter;

        public ItemCheckNameCmd(IWindowPresenter presenter, string parentPath)
        {
            _presenter = presenter;
            _parentPath = parentPath;
        }

        public virtual void Execute(object param = null)
        {
            var keyData = GetDateName();
            var keyPath = GetPath(keyData);

            Check(keyPath, keyData);
        }
        
        private protected virtual string GetDateName() => _presenter.InputString;

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
                _presenter.СonfirmCmd.Execute(keyData);
            else
                _presenter.AcceptCmd.Execute(keyData);
        }
    }
}