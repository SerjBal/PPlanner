using static System.IO.Path;
namespace SerjBal
{
    public class SettingsSplitButton : SplitButtonPresenter, IHierarchical
    {
        public override void Initialize(ButtonView view, Services services)
        {
            InitializeCommands(services);
            InitializeBaseView(view, GetFileName(Path));
            InitializeConfigsView(view);
        }

        private void InitializeConfigsView(ButtonView view)
        {
            var configsView = view as SettingsButtonView;
            if (configsView) configsView.button.onClick.AddListener(PushButton);
        }

        private void InitializeCommands(Services services)
        {
            RemoveCommand = default;
            EditCommand = default;
            CollapseEndCommand = new ButtonCollapseEndCmd(this);
            CollapseStartCommand = new ButtonCollapseStartCmd(this);
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd(this);
            ContentUpdateCommand = new ConfigsUpdateCmd(this, services);
            AddNewContentCommand = default;
        }
    }
}