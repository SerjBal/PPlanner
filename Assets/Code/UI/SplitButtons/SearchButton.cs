using System.Threading.Tasks;
using SerjBal.Searching;

namespace SerjBal
{
    public class SearchButton : ButtonViewModel, IHierarchical
    {
        private ISearchingEngine _searchEngine;

        public override void Initialize(Services services)
        {
            _searchEngine = services.Single<ISearchingEngine>();
            CollapseEndCommand = new ButtonCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd(this);
            ContentUpdateCommand = new SearchContentUpdateCmd(this, services);
            AddNewContentCommand = default;
        }

        public void SearchInitialize(string value)
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

        public void ShowResults(bool isFounded)
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