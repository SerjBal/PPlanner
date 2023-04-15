using static System.IO.Path;
namespace SerjBal
{
    public class CommentButton : ButtonViewModel, IHierarchical
    {
        public override void Initialize(ButtonView view, Services services)
        {
            ItemType = MenuItemType.Time;
            InitializeCommands(services);
            InitializeView(view, GetFileName(Path));
        }
        private void InitializeCommands(Services services)
        {
            RemoveCommand = default;
            EditCommand = new ButtonEditCmd<EditChannelWindow>(this, services);
            CollapseEndCommand = new ButtonCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd( this);
            ContentUpdateCommand = new TextEditorAddCmd(this, services);
            AddNewContentCommand = default;
        }
    }
}