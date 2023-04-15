using static System.IO.Path;
using SerjBal.Searching;

namespace SerjBal
{
    public class SearchButton : ButtonViewModel, IHierarchical
    {
        private ISearchingEngine _searchEngine;
        
        public override void Initialize(ButtonView view, Services services)
        {
            _searchEngine = services.Single<ISearchingEngine>();
            InitializeCommands(services);
            InitializeView(view, GetFileName(Path));
            InitializeSearchView(view);
        }

        private void InitializeSearchView(ButtonView view)
        {
            var searchView = view as SearchButtonView;
            if (searchView) searchView.inputField.onValueChanged.AddListener(SearchInitialize);
        }

        private void InitializeCommands(Services services)
        {
            CollapseEndCommand = new ButtonCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd(this);
            ContentUpdateCommand = new SearchContentUpdateCmd(this, services);
            AddNewContentCommand = default;
        }

        private void SearchInitialize(string value)
        {
            if (value.Length > 0)
                SearchStart(value);
            else
            if (IsSelected) PushButton();
        }

        private async void SearchStart(string value)
        {
            var isFounded = await _searchEngine.Search(value);
            ShowResults(isFounded);
        }

        private void ShowResults(bool isFounded)
        {
            if (isFounded)
            {
                ContentUpdateCommand.Execute();
                if (!IsSelected) PushButton();
            }
            else if (IsSelected) PushButton();
        }
        
    }
}