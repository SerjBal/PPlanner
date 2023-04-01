using System.IO;

namespace SerjBal
{
    public class ButtonOverrideCmd : ICommand
    {
        private readonly IDataProvider _data;
        private readonly IHierarchical _itemViewModel;
        private readonly IWindowViewModel _viewModel;

        public ButtonOverrideCmd(IWindowViewModel viewModel, Services services, IHierarchical itemViewModel)
        {
            _viewModel = viewModel;
            _itemViewModel = itemViewModel;
            _data = services.Single<IDataProvider>();
        }

        public void Execute(object param = null)
        {
            var oldPath = _itemViewModel.Path;
            var newPath = ChangeDirectoryName(oldPath);
            if (oldPath!=newPath) 
                _data.MoveDirectory(oldPath, newPath);

            (_itemViewModel.Parent as ButtonViewModel)?.ContentUpdateCommand.Execute();
            _viewModel.OnClose.Invoke();
        }

        private string ChangeDirectoryName(string oldPath)
        {
            var root = Directory.GetParent(oldPath);
            return Path.Combine(root.FullName, _viewModel.InputString);
        }
    }
}