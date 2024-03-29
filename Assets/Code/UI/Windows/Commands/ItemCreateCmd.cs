using System.IO;
using UnityEngine;

namespace SerjBal
{
    public class ButtonCreateCmd : ICommand
    {
        private readonly IDataProvider _dataProvider;
        private protected readonly IHierarchical itemViewModel;
        private protected readonly IWindowPresenter presenter;

        public ButtonCreateCmd(IWindowPresenter presenter, Services services, IHierarchical itemViewModel)
        {
            this.presenter = presenter;
            this.itemViewModel = itemViewModel;
            _dataProvider = services.Single<IDataProvider>();
        }

        public virtual void Execute(object param = null)
        {
            if (param != null)
            {
                CreateDirectory((string)param);
                UpdateItemContent();
                CloseWindow();
            }
            else
                Debug.LogError("Parameter is null");
        }

        private void CloseWindow() => presenter.OnClose.Invoke();

        private void UpdateItemContent()
        {
            ((SplitButtonPresenter)itemViewModel)
                .ContentUpdateCommand.Execute();
        }

        private void CreateDirectory(string newItem)
        {
            var newPath = Path.Combine(itemViewModel.ContentPath, newItem);
            _dataProvider.CreateDirectory(newPath);
        }
    }
}