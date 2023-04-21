using static System.IO.Path;
namespace SerjBal
{
    public class TemplateSplitButton : SplitButtonPresenter, IHierarchical
    {
        
        public override void Initialize(ButtonView view, Services services)
        {
            ItemType = MenuItemType.Date;
            InitializeCommands(services);
            InitializeBaseView(view, GetFileName(Path));
        }
        private void InitializeCommands(Services services)
        {
            RemoveCommand = new ButtonRemoveCmd(this, services);
            EditCommand = new ButtonEditCmd<EditTemplateNameWindow>(this, services);
            CollapseEndCommand = default;
            CollapseStartCommand = default;
            ExpandEndCommand = default;
            ExpandStartCommand = default;
            ContentUpdateCommand = new LoadTemplateCmd(this, services);
            AddNewContentCommand = default;
            OnExpandStateChanged += LoadTemplate;
        }

        private void LoadTemplate(bool isTrue) => ContentUpdateCommand.Execute();
    }
}