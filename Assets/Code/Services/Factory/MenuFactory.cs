using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class MenuFactory : IMenuFactory
    {
        private readonly Configurations _configs;
        private readonly Services _services;
        private readonly IAssetsProvider _assets;
        private ITemplatesProvider _templates;

        public MenuFactory(Services services, Configurations configs)
        {
            _services = services;
            _configs = configs;
            _assets = _services.Single<IAssetsProvider>();
        }

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(Const.DateItemPath);
            await _assets.Load<GameObject>(Const.ChannelItemPath);
            await _assets.Load<GameObject>(Const.TimeItemPath);
            await _assets.Load<GameObject>(Const.TextItemPath);
            await _assets.Load<GameObject>(Const.AddItemButtonPath);
            await _assets.Load<GameObject>(Const.SearchResultItemPath);
            await _assets.Load<GameObject>(Const.CommentsButtonPath);
        }

        public async Task<MainMenuViewModel> CreateMainMenu()
        {
            var mainMenuVieModel = new MainMenuViewModel();
            var guiObject = GameObject.FindGameObjectWithTag(Const.GUITag)
                            ?? await _assets.Instantiate(Const.GUIPath);

            guiObject.GetComponent<MainMenuView>()?.Setup(mainMenuVieModel, _services, _configs);
            return mainMenuVieModel;
        }

        public async Task<IHierarchical> CreateButton(IHierarchical parent, string path)
        {
            ButtonViewModel buttonViewModel;
            ButtonView buttonView;
            
            switch (parent.ItemType)
            {
                case MenuItemType.None:
                    return await CreateButton<DateButton>(Const.DateItemPath, parent, path);
                
                case MenuItemType.Date:
                    return await CreateChannel(parent, path);
                
                case MenuItemType.Channel:
                    var timeViewModel = await CreateButton<TimeButton>(Const.TimeItemPath, parent, path);
                    timeViewModel.CreateMetaData();
                    return timeViewModel;
                
                case MenuItemType.Search:
                    buttonViewModel = CreateViewModel<SearchResultButton>(parent, path);
                    buttonView = await CreateView<SearchResultButtonView>(Const.SearchResultItemPath, parent);
                    buttonView.ReleaseSetup(buttonViewModel);
                    return (IHierarchical)buttonViewModel;
                
                default:
                    Debug.LogError("Wrong button type");
                    return null;
            }
        }

        private async Task<IHierarchical> CreateChannel(IHierarchical parent, string path)
        {
            var buttonViewModel = CreateViewModel<ChannelButton>(parent, path);
            var channelView = await CreateView<ButtonView>(Const.ChannelItemPath, parent);
            channelView.ReleaseSetup(buttonViewModel);
            var widget = channelView.GetComponent<PostsWidget>();
            widget.Initialize(_services, buttonViewModel, _configs.indicatorsConfig);
            buttonViewModel.SetWidget(widget);
            buttonViewModel.UpdateWidget();
            return buttonViewModel;
        }

        public async Task<TButton> CreateButton<TButton>(string addressablePath, IHierarchical parent, string path) where TButton : ButtonViewModel, new()
        {
            var buttonViewModel = CreateViewModel<TButton>(parent, path);
            var buttonView = await CreateView<ButtonView>(addressablePath, parent);
            buttonView.ReleaseSetup(buttonViewModel);
            return buttonViewModel;
        }
        
        public async Task<TextEditorViewModel> CreateTextEditor(IHierarchical parent, string path)
        {
            var textItem = await _assets.Instantiate<TextEditorViewModel>(Const.TextItemPath, parent.ContentContainer);
            textItem.Parent = parent;
            textItem.Initialize(path);
            return textItem;
        }

        public async Task<Button> CreateAddButton(Transform parent) => 
            await _assets.Instantiate<Button>(Const.AddItemButtonPath, parent);
        
        private TButton CreateViewModel<TButton>(IHierarchical parent, string path) where TButton : ButtonViewModel, new()
        {
            var itemViewModel = new TButton { Parent = parent, Path = path };
            itemViewModel.Initialize(_services);
            return itemViewModel;
        }
        
        private async Task<TView> CreateView<TView>(string addressablePath, IHierarchical parent) where TView : IView
        {
            var buttonView = await _assets.Instantiate<TView>(addressablePath, parent.ContentContainer);
            buttonView.Initialize(_configs.buttonConfig);
            return buttonView;
        }

    }
}