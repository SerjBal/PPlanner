using System.Collections.Generic;

namespace SerjBal
{
    public class SearchContentUpdateCmd : ICommand
    {
        private readonly IMenuFactory _menuFactory;
        private readonly SearchEngine _searchEngine;
        private readonly ButtonViewModel _viewModel;
        private readonly IHierarchical _item;

        public SearchContentUpdateCmd(IHierarchical item, Services services, SearchEngine searchEngine)
        {
            _viewModel = item as ButtonViewModel;
            _item = item;
            _searchEngine = searchEngine;
            _menuFactory = services.Single<IMenuFactory>();
        }

        public async void Execute(object param = null)
        {
            _item.ContentContainer.Clear();
            _item.ChildList = new List<IHierarchical>();

            var content = _searchEngine.GetResults();

            foreach (var result in content) 
                _item.ChildList.Add(await _menuFactory.CreateButton(_item, result.Key));

            _viewModel.ExpandEndCommand?.Execute();
        }
    }
}