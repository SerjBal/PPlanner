
namespace SerjBal
{
    public class PostsColorSettingsPresenter
    {
        private IWindowsFactory _factory;
        private PostsColorSettingsView _view;
        private ISettingsProvider _setting;

        public void Initialize(PostsColorSettingsView view, Services services)
        {
            _view = view;
            _setting = services.Single<ISettingsProvider>();
            _factory = services.Single<IWindowsFactory>();
            _view.contentUndoneButton.onClick.AddListener(() => CreateColorSelectorWindow(ColorSettingType.ContentUndone));
            _view.contentDoneButton.onClick.AddListener(() => CreateColorSelectorWindow(ColorSettingType.ContentDone));
            _view.adsUndoneButton.onClick.AddListener(() => CreateColorSelectorWindow(ColorSettingType.AdsUndone));
            _view.adsDoneButton.onClick.AddListener(() => CreateColorSelectorWindow(ColorSettingType.AdsDone));
            _view.timeProgressButton.onClick.AddListener(() => CreateColorSelectorWindow(ColorSettingType.TimeProgress));
            UpdateView();
        }

        private void CreateColorSelectorWindow(ColorSettingType settingType) => 
            _factory.CreateColorSelectorWindow(UpdateView, settingType);

        public void UpdateView()
        {
            _view.contentUndoneColor.color = _setting.LoadColorSettings(ColorSettingType.ContentUndone);
            _view.contentDoneColor.color = _setting.LoadColorSettings(ColorSettingType.ContentDone);
            _view.adsUndoneColor.color = _setting.LoadColorSettings(ColorSettingType.AdsUndone);
            _view.adsDoneColor.color = _setting.LoadColorSettings(ColorSettingType.AdsDone);
            _view.timeProgressColor.color = _setting.LoadColorSettings(ColorSettingType.TimeProgress);
        }
    }
}