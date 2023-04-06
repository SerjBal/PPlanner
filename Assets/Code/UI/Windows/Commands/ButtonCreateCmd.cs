using System.IO;
using UnityEngine;

namespace SerjBal
{
    public class ButtonCreateCmd : ICommand
    {
        private readonly IDataProvider _dataProvider;
        private readonly IHierarchical _itemViewModel;
        private readonly IWindowViewModel _viewModel;

        public ButtonCreateCmd(IWindowViewModel viewModel, Services services, IHierarchical itemViewModel)
        {
            _viewModel = viewModel;
            _itemViewModel = itemViewModel;
            _dataProvider = services.Single<IDataProvider>();
        }

        public void Execute(object param = null)
        {
            if (param != null)
            {
                CreateDirectory((string)param);
                UpdateItemContent();
                CloseWindow();
            }
            else
            {
                Debug.LogError("Parameter is null");
            }
        }

        private void CloseWindow()
        {
            _viewModel.OnClose.Invoke();
        }

        private void UpdateItemContent()
        {
            ((ButtonViewModel)_itemViewModel)
                .ContentUpdateCommand.Execute();
        }

        private void CreateDirectory(string newItem)
        {
            var newPath = Path.Combine(_itemViewModel.ContentPath, newItem);
            _dataProvider.CreateDirectory(newPath);
        }
    }
}