namespace SerjBal
{
    public class DateButton : ButtonViewModel, IHierarchical
    {
        public override void Initialize(Services services)
        {
            ItemType = MenuItemType.Date;

            SelectCommand = new ButtonSelectCmd(this);
            RemoveCommand = new ButtonRemoveCmd(this, services);
            EditCommand = new ButtonEditCmd<EditDateWindow>(this, services);
            CollapseFinishEnd = new ButtonCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd(this);
            ContentUpdateCommand = new ButtonUpdateCmd(this, services);
            AddNewContentCommand = new ButtonEditCmd<NewChannelWindow>(this, services);
        }
    }
}