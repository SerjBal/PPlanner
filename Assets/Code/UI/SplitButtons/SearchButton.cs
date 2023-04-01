using System.Threading.Tasks;

namespace SerjBal
{
    public class SearchButton : ButtonViewModel, IHierarchical
    {
        private SearchEngine _searchEngine;

        public override void Initialize(Services services)
        {
            ItemType = MenuItemType.Search;
            
            _searchEngine = new SearchEngine(services);
            SelectCommand = new ButtonSelectCmd(this);
            CollapseFinishEnd = new ButtonCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd(this);
            ContentUpdateCommand = new SearchContentUpdateCmd(this, services, _searchEngine);
            AddNewContentCommand = default;
        }

        public void SearchInitialize(string value)
        {
            if (value.Length > 0)
                SearchStart(value);
            else
                IsSelected = false;
        }

        private async Task SearchStart(string value)
        {
            var isFounded = await _searchEngine.Search(value);
            ShowResults(isFounded);
        }

        public void ShowResults(bool isFounded)
        {
            if (isFounded)
            {
                ContentUpdateCommand.Execute();
                IsSelected = true;
            }
            else
            {
                IsSelected = false;
            }
        }
    }
}