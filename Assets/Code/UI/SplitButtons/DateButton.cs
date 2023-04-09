namespace SerjBal
{
    public class DateButton : ButtonViewModel, IHierarchical
    {
        public override void Initialize(Services services)
        {
            ItemType = MenuItemType.Date;

            RemoveCommand = new ButtonRemoveCmd(this, services);
            EditCommand = new ButtonEditCmd<EditDateWindow>(this, services);
            CollapseEndCommand = new ButtonCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd(this);
            ContentUpdateCommand = new DateUpdateCmd(this, services);
            AddNewContentCommand = new ButtonEditCmd<NewChannelWindow>(this, services);
        }
    }
}