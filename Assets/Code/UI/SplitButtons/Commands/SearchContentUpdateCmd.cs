using System.Collections.Generic;
using SerjBal.Searching;

namespace SerjBal
{
    public class SearchContentUpdateCmd : ICommand
    {
        private readonly IMenuFactory _menuFactory;
        private readonly ISearchingEngine _searchEngine;
        private readonly SplitButtonPresenter _presenter;
        private readonly IHierarchical _item;

        public SearchContentUpdateCmd(IHierarchical item, Services services)
        {
            _presenter = item as SplitButtonPresenter;
            _item = item;
            _searchEngine = services.Single<ISearchingEngine>();
            _menuFactory = services.Single<IMenuFactory>();
        }

        public async void Execute(object param = null)
        {
            _item.ContentContainer.Clear();
            _item.ChildList = new List<IHierarchical>();

            var content = _searchEngine.GetResults();

            foreach (var result in content) 
                _item.ChildList.Add(await _menuFactory.CreateSearchResultButton(_item, result.Key));

            _presenter.ExpandEndCommand?.Execute();
        }
    }
}