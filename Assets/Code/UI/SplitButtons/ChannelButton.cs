using static System.IO.Path;
namespace SerjBal
{
    public class ChannelButton : ButtonViewModel, IHierarchical
    {
        private PostsWidget _widget;
        
        public override void Initialize(ButtonView view, Services services)
        {
            ItemType = MenuItemType.Channel;
            InitializeCommands(services);
            InitializeView(view, GetFileName(Path));

            var configs = Configurations.Instance.indicatorsConfig;
            _widget = view.GetComponent<PostsWidget>();
            _widget.Initialize(services, this, configs);
            UpdateWidget();
        }

        private void InitializeCommands(Services services)
        {
            RemoveCommand = new ButtonRemoveCmd(this, services);
            EditCommand = new ButtonEditCmd<EditChannelWindow>(this, services);
            CollapseEndCommand = new ButtonCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd(this);
            ContentUpdateCommand = new ChannelUpdateCmd(this, services);
            AddNewContentCommand = new TimeEditCmd<NewTimeWindow>(this, services);
        }

        public void UpdateWidget() => _widget.UpdateView();
    }
}