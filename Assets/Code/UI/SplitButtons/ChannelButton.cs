namespace SerjBal
{
    public class ChannelButton : ButtonViewModel, IHierarchical
    {
        private PostsWidget _widget;

        public override void Initialize(Services services)
        {
            ItemType = MenuItemType.Channel;

            RemoveCommand = new ButtonRemoveCmd(this, services);
            EditCommand = new ButtonEditCmd<EditChannelWindow>(this, services);
            CollapseEndCommand = new ButtonCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd(this);
            ContentUpdateCommand = new ChannelUpdateCmd(this, services);
            AddNewContentCommand = new TimeEditCmd<NewTimeWindow>(this, services);
        }

        public void SetWidget(PostsWidget widget) => _widget = widget;

        public void UpdateWidget() => _widget.UpdateView();
    }
}