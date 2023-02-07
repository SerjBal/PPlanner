using System;
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
        private readonly ISaveLoad _saveLoad;
        private ITemplatesProvider _templates;
        private MainMenuItemView _GUI;

        public AppFactory(IAssetsProvider assets,  IDataProvider data, ISaveLoad saveLoad, Configurations configs)
        {
            _assets = assets;
            _data = data;
            _saveLoad = saveLoad;
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
            await _assets.Load<GameObject>(Const.NewTimeWindowPath);
            await _assets.Load<GameObject>(Const.WarningWindowPath);
        }
        
        public async Task<MainMenuItemView> CreateGUI()
        {
            GameObject prefab = await _assets.Instantiate(Const.GUIPath);
            MainMenuItemView mainMenuItemView = prefab.GetComponent<MainMenuItemView>();
            _GUI = mainMenuItemView;
            return mainMenuItemView;
        }

        
        public async Task CreateEditDateWindow(IMenuItemViewModel menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.EditWindowPath, null);
            window.SetEditFormatText(Const.EditDateWindowFormatText);
            window.SetAcceptButtonText(Const.EditDateWindowButtonText);
            window.Initialize(menuItem);
        }

        public async Task CreateEditChannelWindow(IMenuItemViewModel menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.EditWindowPath, null);
            window.SetEditFormatText(Const.EditChannelWindowFormatText);
            window.SetAcceptButtonText(Const.EditChannelWindowButtonText);
            window.Initialize(menuItem);
        }
        
        public async Task CreateNewChannelWindow(IMenuItemViewModel menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.NewChannelWindowPath, null);
            window.SetEditFormatText(Const.NewChannelWindowFormatText);
            window.SetAcceptButtonText(Const.NewChannelWindowButtonText);
            window.Initialize(menuItem);
        }

        public async Task CreateEditTimeWindow(IMenuItemViewModel menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.EditWindowPath, null);
            window.SetEditFormatText(Const.EditTimeWindowFormatText);
            window.SetAcceptButtonText(Const.EditTimeWindowButtonText);
            window.Initialize(menuItem);
        }
        
        
        public async Task CreateNewTimeWindow(IMenuItemViewModel menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.NewTimeWindowPath, null);
            window.SetEditFormatText(Const.NewTimeWindowFormatText);
            window.SetAcceptButtonText(Const.NewTimeWindowButtonText);
            window.Initialize(menuItem);
        }

        public async Task CreateReplacingDataWindow(UnityAction onAccept)
        {
            var window = await _assets.Instantiate<IWarningWindow>(Const.WarningWindowPath, null);
            window.SetWarningText(Const.ReplaceWarningText);
            window.SetAcceptButtonText(Const.ReplaceButtonText);
            window.Initialize(onAccept);
        }

        public async Task<IMenuItemViewModel> CreateDateItem()
        {
            IData dateData = _data.GetDateData();
            IMenuItemViewModel item = await _assets.Instantiate<IMenuItemViewModel>(Const.DateItemPath, _GUI.ContentContainer);
            if (dateData.Content!=null)
            {
                if (dateData.Content.Count>0)
                {
                    var contentList = new Dictionary<string, IMenuItemViewModel>();
                    foreach (string key in dateData.Content.Keys)
                    {
                        contentList.Add(key, await CreateChannelItem(item, key));
                    }
                    item.ContentList = contentList;
                }
            }

            item.Parent = null;
            item.ChangeKey(dateData.Key);
            item.Initialize(_configs.buttonConfigs, this);
            item.ViewTransform.GetComponent<ExpandAnimator>().AnimationPlay();
            return item;
        }
        

        public async Task<IMenuItemViewModel> CreateChannelItem(IMenuItemViewModel parent, string channelKey)
        {
            IData dateData = _data.GetDateData();
            IMenuItemViewModel item = await _assets.Instantiate<IMenuItemViewModel>(Const.ChannelItemPath, parent.ContentContainer);
            if (dateData.Content != null)
            {
                if (_data.GetDateData().Content.ContainsKey(channelKey))
                {
                    IData channelData = dateData.Content[channelKey];
                    if (channelData.Content.Count > 0)
                    {
                        var contentList = new Dictionary<string, IMenuItemViewModel>();
                        foreach (string key in channelData.Content.Keys)
                        {
                            contentList.Add(key, await CreateTimeItem(item, key));
                        }
                        item.ContentList = contentList;
                    }
                }
            }
            item.Parent = parent;
            item.ChangeKey(channelKey);
            item.Initialize(_configs.buttonConfigs, this);
            return item;
        }
        
        public async Task<IMenuItemViewModel> CreateTimeItem(IMenuItemViewModel parent, string timeKey)
        {
            //IData dateData = _data.GetDateData().Content[parent.Key].Content[timeKey];
            IMenuItemViewModel item = await _assets.Instantiate<IMenuItemViewModel>(Const.TimeItemPath, parent.ContentContainer);
            item.Parent = parent;
            item.ChangeKey(timeKey);
            item.Initialize(_configs.buttonConfigs, this);
            return item;
        }   
        
        public async Task CreateTextEditor(IMenuItemViewModel parent, string timeKey)
        {
            TextEditorViewModel viewModel = await _assets.Instantiate<TextEditorViewModel>(Const.TextItemPath, parent.ContentContainer);
            viewModel.Key = timeKey;
        }

        public void CleanUp()
        {
            _assets.Cleanup();
        }
    }
}