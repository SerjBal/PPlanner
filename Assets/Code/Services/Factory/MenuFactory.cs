using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SerjBal.Code.Sources;
using UnityEngine;
using UnityEngine.Events;
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
        }
        
        public async Task<MainMenuItemView> CreateGUI()
        {
            var guiObject = GameObject.FindGameObjectWithTag(Const.GUITag);
            if (guiObject==null) guiObject = await _assets.Instantiate(Const.GUIPath);
            
            _GUI = guiObject.GetComponent<MainMenuItemView>();
            return _GUI;
        }

        public async Task<IMenuItem> CreateDateItem()
        {
            IMenuItem item = await _assets.Instantiate<IMenuItem>(Const.DateItemPath, _GUI.dateContainer);

            item.ChangeKey(_data.GetOrCreateDateData().Key);
            item.Initialize(_configs.buttonConfigs);
            return item;
        }
        
        public async Task<IMenuItem> CreateChannelItem(IMenuItem parent, string channelKey) => 
            await CreateMenuItem(Const.ChannelItemPath, channelKey, parent);

        public async Task<IMenuItem> CreateTimeItem(IMenuItem parent, string timeKey) => 
            await CreateMenuItem(Const.TimeItemPath, timeKey, parent);

        public async Task CreateTextEditor(IMenuItem parent, string timeKey)
        {
            TextEditorViewModel textItem = await _assets.Instantiate<TextEditorViewModel>(Const.TextItemPath, parent.ContentContainer);
            textItem.Key = timeKey;
            textItem.Parent = parent;
            textItem.Initialize();
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
            item.Initialize(_configs.buttonConfigs);
            return item;
        }
    }
}