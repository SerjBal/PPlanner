using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SerjBal.Code.Sources;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SerjBal
{
    public class AppFactory : IAppFactory
    {
        private IAssetsProvider _assets;
        private readonly Configurations _configs;
        private IDataProvider _data;
        private ITemplatesProvider _templates;
        private MainMenuItemView _GUI;

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
            await _assets.Load<GameObject>(Const.AddItemButtonPath);
            
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

        
        public async Task CreateEditDateWindow(IMenuItem menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.EditWindowPath, null);
            window.SetEditFormatText(Const.EditDateWindowFormatText);
            window.SetAcceptButtonText(Const.EditDateWindowButtonText);
            window.Initialize(menuItem);
        }

        public async Task CreateEditChannelWindow(IMenuItem menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.EditWindowPath, null);
            window.SetEditFormatText(Const.EditChannelWindowFormatText);
            window.SetAcceptButtonText(Const.EditChannelWindowButtonText);
            window.Initialize(menuItem);
        }
        
        public async Task CreateNewChannelWindow(IMenuItem menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.NewChannelWindowPath, null);
            window.SetEditFormatText(Const.NewChannelWindowFormatText);
            window.SetAcceptButtonText(Const.NewChannelWindowButtonText);
            window.Initialize(menuItem);
        }

        public async Task CreateEditTimeWindow(IMenuItem menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.EditWindowPath, null);
            window.SetEditFormatText(Const.EditTimeWindowFormatText);
            window.SetAcceptButtonText(Const.EditTimeWindowButtonText);
            window.Initialize(menuItem);
        }
        
        
        public async Task CreateNewTimeWindow(IMenuItem menuItem)
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

        public async Task<IMenuItem> CreateDateItem()
        {
            IMenuItem item = await _assets.Instantiate<IMenuItem>(Const.DateItemPath, _GUI.lowScreenContainer);

            item.ChangeKey(_data.GetDateData().Key);
            item.Initialize(_configs.buttonConfigs, this);
            return item;
        }
        
        public async Task<IMenuItem> CreateChannelItem(IMenuItem parent, string channelKey) => 
            await CreateMenuItem(Const.ChannelItemPath, channelKey, parent);

        public async Task<IMenuItem> CreateTimeItem(IMenuItem parent, string timeKey) => 
            await CreateMenuItem(Const.TimeItemPath, timeKey, parent);

        public async Task CreateTextEditor(IMenuItem parent, string timeKey)
        {
            TextEditorViewModel viewModel = await _assets.Instantiate<TextEditorViewModel>(Const.TextItemPath, parent.ContentContainer);
            viewModel.Key = timeKey;
        }

        public async Task<Button> CreateAddButton(Transform parent)
        {
            return await _assets.Instantiate<Button>(Const.AddItemButtonPath, parent);
        }

        private async Task<IMenuItem> CreateMenuItem(string path, string key = null, IMenuItem parent = null)
        {
            IMenuItem item = await _assets.Instantiate<IMenuItem>(path, parent.ContentContainer);

            item.Parent = parent;
            item.ChangeKey(key);
            item.Initialize(_configs.buttonConfigs, this);
            return item;
        }
    }
}