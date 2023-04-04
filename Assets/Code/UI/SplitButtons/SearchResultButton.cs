namespace SerjBal
{
    public class SearchResultButton : ButtonViewModel, IHierarchical
    {
        public override void Initialize(Services services)
        {
            RemoveCommand = default;
            EditCommand = default;
            CollapseFinishEnd = new SearchCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd( this);
            ContentUpdateCommand = new SearchResultTextAddCmd(this, services);
            AddNewContentCommand = default;
        }
    }
}