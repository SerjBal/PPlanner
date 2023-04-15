using System.IO;

namespace SerjBal
{
    public class ButtonOverrideCmd : ICommand
    {
        private protected readonly IDataProvider data;
        private protected readonly IHierarchical itemViewModel;
        private protected readonly IWindowViewModel viewModel;

        public ButtonOverrideCmd(IWindowViewModel viewModel, Services services, IHierarchical itemViewModel)
        {
            this.viewModel = viewModel;
            this.itemViewModel = itemViewModel;
            data = services.Single<IDataProvider>();
        }

        public virtual void Execute(object param = null)
        {
            var oldPath = itemViewModel.Path;
            var newPath = GetNewPath();
            if (oldPath!=newPath) 
                data.MoveDirectory(oldPath, newPath);

            (itemViewModel.Parent as ButtonViewModel)?.ContentUpdateCommand.Execute();
            viewModel.OnClose.Invoke();
        }

        private protected virtual string GetNewPath()
        {
            var root = Directory.GetParent(itemViewModel.Path);
            return Path.Combine(root.FullName, viewModel.InputString);
        }
    }
}