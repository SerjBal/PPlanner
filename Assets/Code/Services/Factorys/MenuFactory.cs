using System.IO;
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
            await _assets.Load<GameObject>(Const.TemplatesItemPath);
            await _assets.Load<GameObject>(Const.TemplateItemPath);
        }

        public async Task<MainMenuPresenter> CreateMainMenu()
        {
            var guiObject = GameObject.FindGameObjectWithTag(Const.GUITag)
                            ?? await _assets.Instantiate(Const.GUIPath);
            
            var view = guiObject.GetComponent<MainMenuView>();
            var presenter = new MainMenuPresenter(view, _services);
            return presenter;
        }

        public async Task<IHierarchical> CreateMenuButton(IHierarchical parent, string path)
        {
            switch (parent.ItemType)
            {
                case MenuItemType.None:
                    return await CreateButton<DateSplitButton>(Const.DateItemPath, parent, path);
                
                case MenuItemType.Date:
                    return await CreateButton<ChannelSplitButton>(Const.ChannelItemPath, parent, path);
                
                case MenuItemType.Channel:
                    return await CreateButton<TimeSplitButton>(Const.TimeItemPath, parent, path);
                
                default:
                    Debug.LogError("Wrong button type");
                    return null;
            }
        }
        
        public async void CreateTemplatesButton(IHierarchical parent, string path) => 
            await CreateButton<TemplatesRootSplitButton>(Const.TemplatesItemPath, parent, path);

        public async Task<IHierarchical> CreateTemplateButton(IHierarchical parent, string path) => 
            await CreateButton<TemplateSplitButton>(Const.TemplateItemPath, parent, path);

        public async Task<IHierarchical> CreateSearchResultButton(IHierarchical parent, string path) => 
            await CreateButton<SearchResultSplitButton>(Const.SearchResultItemPath, parent, path);

        public async Task<TButton> CreateButton<TButton>(string addressablePath, IHierarchical parent, string path) where TButton : SplitButtonPresenter, new()
        {
            var view = await CreateView<ButtonView>(addressablePath, parent);
            var presenter = CreateViewModel<TButton>(parent, path, view);
            return presenter;
        }

        public async Task<TextEditorPresenter> CreateTextEditor(IHierarchical parent, string path)
        {
            var textItem = await _assets.Instantiate<TextEditorPresenter>(Const.TextItemPath, parent.ContentContainer);
            textItem.Parent = parent;
            textItem.Initialize(path);
            return textItem;
        }

        public async Task<Button> CreateAddButton(Transform parent) => 
            await _assets.Instantiate<Button>(Const.AddItemButtonPath, parent);
        
        private TButton CreateViewModel<TButton>(IHierarchical parent, string path, ButtonView view) where TButton : SplitButtonPresenter, new()
        {
            var presenter = new TButton { Parent = parent, Path = path };
            presenter.Initialize(view, _services);
            return presenter;
        }
        
        private async Task<TView> CreateView<TView>(string addressablePath, IHierarchical parent) where TView : IView
        {
            return await _assets.Instantiate<TView>(addressablePath, parent.ContentContainer);
        }
    }
}