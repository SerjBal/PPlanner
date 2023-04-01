using UnityEngine;

namespace SerjBal
{
    public class SearchResultTextAddCmd : ICommand
    {
        private readonly IMenuFactory _factory;
        private readonly IHierarchical _viewModel;

        public SearchResultTextAddCmd(IHierarchical viewModel, Services services)
        {
            _viewModel = viewModel;
            _factory = services.Single<IMenuFactory>();
        }

        public async void Execute(object param = null)
        {
            _viewModel.ContentContainer.Clear();
            var text = await _factory.CreateTextEditor(_viewModel, _viewModel.Path);
            text.SetReadOnly(true);
        }
    }
}