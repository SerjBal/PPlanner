namespace SerjBal
{
    public class CommentButton : ButtonViewModel, IHierarchical
    {
        public override void Initialize(Services services)
        {
            ItemType = MenuItemType.Time;

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