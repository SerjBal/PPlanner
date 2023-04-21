using static System.IO.Path;
namespace SerjBal
{
    public class TimeSplitButton : SplitButtonPresenter, IHierarchical
    {
        public override void Initialize(ButtonView view, Services services)
        {
            ItemType = MenuItemType.Time;
            InitializeCommands(services);
            InitializeBaseView(view, GetFileName(Path));
        }

        private void InitializeCommands(Services services)
        {
            RemoveCommand = new ButtonRemoveCmd(this, services);
            EditCommand = new TimeEditCmd<EditTimeWindow>(this, services);
            CollapseEndCommand = new ButtonCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd( this);
            ContentUpdateCommand = new TextEditorAddCmd(this, services);
            AddNewContentCommand = default;
        }
    }
}