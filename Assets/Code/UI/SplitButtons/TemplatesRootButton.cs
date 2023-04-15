using static System.IO.Path;
namespace SerjBal
{
    public class TemplatesRootButton : ButtonViewModel, IHierarchical
    {
        public override void Initialize(ButtonView view, Services services)
        {
            ItemType = MenuItemType.Date;
            InitializeCommands(services);
            InitializeView(view, GetFileName(Path));
        }

        private void InitializeCommands(Services services)
        {
            RemoveCommand = default;
            EditCommand = default;
            CollapseEndCommand = new ButtonCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd(this);
            ContentUpdateCommand = new TemplatesButtonUpdateCmd(this, services);
            AddNewContentCommand = new ButtonEditCmd<NewTemplateWindow>(this, services);
        }
    }
}