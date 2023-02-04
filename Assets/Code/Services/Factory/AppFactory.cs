using System.Collections.Generic;
using System.Threading.Tasks;
using SerjBal.Code.Sources;
using UnityEngine;
using UnityEngine.Events;

namespace SerjBal
{
    public class AppFactory : IAppFactory
    {
        private IAssetsProvider _assets;
        private readonly Configurations _configs;
        private IDataProvider _data;
        private ITemplatesProvider _templates;
        private MainView _GUI;

        public AppFactory(IAssetsProvider assets,  IDataProvider data, Configurations configs)
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
            await _assets.Load<GameObject>(Const.EditWindowPath);
            await _assets.Load<GameObject>(Const.NewChannelWindowPath);
            await _assets.Load<GameObject>(Const.NewPostWindowPath);
        }
        
        public async Task<MainView> CreateGUI()
        {
            GameObject prefab = await _assets.Instantiate(Const.GUIPath);
            MainView mainView = prefab.GetComponent<MainView>();
            _GUI = mainView;
            return mainView;
        }

        
        public async Task CreateEditDateWindow(IViewModel menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.EditWindowPath, null);
            window.SetEditFormatText(Const.EditDateWindowFormatText);
            window.SetAcceptButtonText(Const.EditDateWindowButtonText);
            window.Initialize(this, menuItem);
        }
        
        public async Task CreateNewChannelWindow(IViewModel menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.NewChannelWindowPath, null);
            window.SetEditFormatText(Const.NewChannelWindowFormatText);
            window.SetAcceptButtonText(Const.NewChannelWindowButtonText);
            window.Initialize(this, menuItem);
        }
        
        public async Task CreateNewPostWindow(IViewModel menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.NewPostWindowPath, null);
            window.SetEditFormatText(Const.NewPostWindowFormatText);
            window.SetAcceptButtonText(Const.NewPostlWindowButtonText);
            window.Initialize(this, menuItem);
        }

        public async Task<IViewModel> CreateDateItem(IViewModel parent)
        {
            IData dateData = _data.GetDateData();
            IViewModel item = await _assets.Instantiate<IViewModel>(Const.DateItemPath, parent.ContentContainer);
            if (dateData.Content!=null)
            {
                if (dateData.Content.Count>0)
                {
                    foreach (string key in dateData.Content.Keys)
                    {
                        await CreateChannelItem(item, key);
                    }
                }
            }
          
            item.Key = dateData.Key;
            //parent.AddToList(item);
            item.Initialize(_configs.buttonConfigs, this);
            return item;
        }
        

        public async Task<IViewModel> CreateChannelItem(IViewModel parent, string channelKey)
        {
            IData dateData = _data.GetDateData();
            IViewModel item = await _assets.Instantiate<IViewModel>(Const.ChannelItemPath, parent.ContentContainer);
            if (dateData.Content != null)
            {
                if (_data.GetDateData().Content.ContainsKey(channelKey))
                {
                    IData channelData = dateData.Content[channelKey];
                    if (channelData.Content.Count > 0)
                    {
                        foreach (string key in channelData.Content.Keys)
                        {
                            await CreateTimeItem(item, key);
                        }
                    }
                }
            }

            item.ChangeKey(channelKey);
            item.Initialize(_configs.buttonConfigs, this);
            return item;
        }
        
        public async Task<IViewModel> CreateTimeItem(IViewModel parent, string timeKey)
        {
            IData dateData = _data.GetDateData().Content[parent.Key].Content[timeKey];
            IViewModel item = await _assets.Instantiate<IViewModel>(Const.DateItemPath, parent.ContentContainer);
            return item;
        }
        
        public async Task CreateTextEditor(string chunnelID, string timeID)
        {
            TextEditorViewModel viewModel = await _assets.Instantiate<TextEditorViewModel>(Const.TextItemPath, null);
        }

        public void CleanUp()
        {
            _assets.Cleanup();
        }
    }
}