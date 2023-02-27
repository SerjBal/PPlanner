
using System.Text;
using System.Threading.Tasks;
using SerjBal.Code.Sources;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class MenuFactory : IMenuFactory
    {
        private IAssetsProvider _assets;
        private readonly Configurations _configs;
        private IDataProvider _data;
        private ITemplatesProvider _templates;
        private MainMenuItemView _GUI;

        public MenuFactory(IAssetsProvider assets,  IDataProvider data, Configurations configs)
        {
            _assets = assets;
            _data = data;
            _configs = configs;
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
        
        public async Task<MainMenuItemView> CreateGUI()
        {
            var guiObject = GameObject.FindGameObjectWithTag(Const.GUITag);
            if (guiObject==null) guiObject = await _assets.Instantiate(Const.GUIPath);
            
            _GUI = guiObject.GetComponent<MainMenuItemView>();
            return _GUI;
        }

        public Task<IMenuItem> CreateMenuItem(IMenuItem parent, string key)
        {
            switch (parent.itemType)
            {
                case MenuItemType.Date:
                    return CreateChannelItem(parent, key);
                case MenuItemType.Channel:
                    return CreateTimeItem(parent, key);
                case MenuItemType.Time:
                default:
                    return null;
            }
        } 

        public async Task<IMenuItem> CreateDateItem()
        {
            IMenuItem item = await _assets.Instantiate<IMenuItem>(Const.DateItemPath, _GUI.dateContainer);
            
            item.SetName(_data.GetOrCreateDateData().Key);
            item.Initialize(_configs.buttonConfigs);
            return item;
        }

        public async Task CreateTextEditor(IMenuItem parent, string timeKey)
        {
            TextEditorViewModel textItem = await _assets.Instantiate<TextEditorViewModel>(Const.TextItemPath, parent.ContentContainer);
            textItem.Parent = parent;
            textItem.Initialize(timeKey);
        }

        public async Task<IMenuItem> CreateSearchResultItem(IMenuItem parent, TextData data)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0}.{1}.{2}/{3}/{4}", data.year, data.month, data.day, data.channel, data.time);
            string key = stringBuilder.ToString();
            
            var item = await _assets.Instantiate<SearchResultItem>(Const.SearchResultItemPath, parent.ContentContainer);
            item.Parent = parent;
            item.SetName(key);
            item.Initialize(_configs.buttonConfigs);
            item.Setup(data);
            return item;
        }

        public async Task<Button> CreateAddButton(Transform parent) => 
            await _assets.Instantiate<Button>(Const.AddItemButtonPath, parent);
        
        private async Task<IMenuItem> CreateChannelItem(IMenuItem parent, string channelKey) => 
            await CreateMenuItemInstance(Const.ChannelItemPath, channelKey, parent);

        private async Task<IMenuItem> CreateTimeItem(IMenuItem parent, string timeKey) => 
            await CreateMenuItemInstance(Const.TimeItemPath, timeKey, parent);
        private async Task<IMenuItem> CreateMenuItemInstance(string path, string key = null, IMenuItem parent = null)
        {
            IMenuItem item = await _assets.Instantiate<IMenuItem>(path, parent.ContentContainer);
            
            item.Parent = parent;
            item.SetName(key);
            item.Initialize(_configs.buttonConfigs);
            return item;
        }
    }

    public interface ISearchItem
    {
        void Setup(TextData data);
    }
}