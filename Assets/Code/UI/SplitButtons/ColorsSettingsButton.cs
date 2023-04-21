using static System.IO.Path;
namespace SerjBal
{
    public class ColorsSettingsButton : SplitButtonPresenter, IHierarchical
    {
        private PostsColorSettingsPresenter _postsColorsSettings;
        public override void Initialize(ButtonView view, Services services)
        {
            InitializeCommands();
            InitializeBaseView(view, Const.ColosSettingsName);
            InitializeSettingsView(view, services);
        }
        
        private void InitializeSettingsView(ButtonView view, Services services)
        {
            var configsView = (ColorSettingsButtonView)view;
            configsView.button.onClick.AddListener(PushButton);
            _postsColorsSettings = new PostsColorSettingsPresenter();
            _postsColorsSettings.Initialize(configsView.postsColorSettings, services);
        }

        private void InitializeCommands()
        {
            RemoveCommand = default;
            EditCommand = default;
            CollapseEndCommand = new BlankCollapseEndCmd(this);
            CollapseStartCommand = default;
            ExpandEndCommand = new ButtonExpandEndCmd();
            ExpandStartCommand = new ButtonExpandStartCmd(this);
            ContentUpdateCommand = default;
            AddNewContentCommand = default;
        }
    }
}