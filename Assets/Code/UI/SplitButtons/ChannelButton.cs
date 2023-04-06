namespace SerjBal
{
    public class ChannelButton : ButtonViewModel, IHierarchical
    {
        public override void Initialize(Services services)
        {
            ItemType = MenuItemType.Channel;

            RemoveCommand = new ButtonRemoveCmd(this, services);
            EditCommand = new ButtonEditCmd<EditChannelWindow>(this, services);
            CollapseFinishEnd = new ButtonCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd(this);
            ContentUpdateCommand = new ChannelUpdateCmd(this, services);
            AddNewContentCommand = new ButtonEditCmd<NewTimeWindow>(this, services);
        }
    }
}