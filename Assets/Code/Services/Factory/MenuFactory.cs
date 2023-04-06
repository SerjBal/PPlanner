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
            switch (parent.ItemType)
            {
                case MenuItemType.None:
                    return await CreateButton<DateButton>(Const.DateItemPath, parent, path);
                case MenuItemType.Date:
                    return await CreateButton<ChannelButton>(Const.ChannelItemPath, parent, path);
                case MenuItemType.Channel:
                    return await CreateButton<TimeButton>(Const.TimeItemPath, parent, path);
                case MenuItemType.Search:
                    var itemViewModel = CreateViewModel<SearchResultButton>(parent, path);
                    var itemView = await _assets.Instantiate<SearchResultButtonView>(Const.SearchResultItemPath, parent.ContentContainer);
                    itemView.Setup(itemViewModel);
                    itemView.Initialize(_configs.buttonConfig);
                    return itemViewModel as IHierarchical;
                default:
                    Debug.LogError("Wrong button type");
                    return null;
            }
        }
        
        public async Task<IHierarchical> CreateButton<TButton>(string addressablePath, IHierarchical parent, string path) where TButton : ButtonViewModel, new()
        {
            var itemViewModel = CreateViewModel<TButton>(parent, path);
            var itemView = await _assets.Instantiate<ButtonView>(addressablePath, parent.ContentContainer);
            itemView.Setup(itemViewModel);
            itemView.Initialize(_configs.buttonConfig);
            return itemViewModel as IHierarchical;
        }

        private ButtonViewModel CreateViewModel<TButton>(IHierarchical parent, string path) where TButton : ButtonViewModel, new()
        {
            ButtonViewModel itemViewModel = new TButton();
            itemViewModel.Parent = parent;
            itemViewModel.Path = path;
            itemViewModel.Initialize(_services);
            return itemViewModel;
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

    }
}