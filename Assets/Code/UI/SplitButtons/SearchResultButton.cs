using static System.IO.Path;
namespace SerjBal
{
    public class SearchResultButton : ButtonViewModel, IHierarchical
    {
        public override void Initialize(ButtonView view, Services services)
        {
            InitializeCommands(services);
            InitializeView(view, GetRelativePath(Const.DataPath, Path));
        }
        private void InitializeCommands(Services services)
        {
            RemoveCommand = default;
            EditCommand = default;
            CollapseEndCommand = new SearchCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd( this);
            ContentUpdateCommand = new SearchResultTextAddCmd(this, services);
            AddNewContentCommand = default;
        }
    }
}