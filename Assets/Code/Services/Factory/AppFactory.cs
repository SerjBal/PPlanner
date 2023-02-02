using System.Collections.Generic;
using System.Threading.Tasks;
using SerjBal.Code.Sources;
using UnityEngine;

namespace SerjBal
{
    public class AppFactory : IAppFactory
    {
        private IAssetsProvider _assets;
        private readonly Configurations _configs;
        private IDataProvider _data;
        private ITemplatesProvider _templates;
        private MainView _GUI;

        public AppFactory(IAssetsProvider assets, Configurations configs)
        {
            _assets = assets;
            _configs = configs;
        }
        
        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(Const.DateItemPath);
            await _assets.Load<GameObject>(Const.ChannelItemPath);
            await _assets.Load<GameObject>(Const.TimeItemPath);
            await _assets.Load<GameObject>(Const.TextItemPath);
        }
        
        public async Task<MainView> CreateGUI()
        {
            GameObject prefab = await _assets.Instantiate(Const.GUIPath);
            MainView mainView = prefab.GetComponent<MainView>();

            _GUI = mainView;
            return mainView;
        }
        

        public async Task<IViewModel> CreateDateItem(IViewModel parent)
        {
            IData dateData = _data.GetDateData();
            IViewModel item = await CreateMenuItem(parent, dateData);
            return item;
        }

        public async Task<IViewModel> CreateChannelItem(IViewModel parent)
        {
            IData dateData = _data.GetDateData();
            IViewModel item = await CreateMenuItem(parent, dateData);
            return item;
        }
        
        public async Task<IViewModel> CreateTimeItem(IViewModel parent)
        {
            IData dateData = _data.GetDateData();
            IViewModel item = await CreateMenuItem(parent, dateData);
            return item;
        }
        
        public async Task CreateTextEditor(string chunnelID, string timeID)
        {
            TextEditorViewModel viewModel = await CreateItem<TextEditorViewModel>(Const.TextItemPath);
        }
        
        private async Task<IViewModel> CreateMenuItem(IViewModel parent, IData data)
        {
            IViewModel viewModel = await CreateItem<IViewModel>(Const.DateItemPath);
            parent.Add(viewModel);
            if (data.Content.Count>0)
            {
                foreach (string key in data.Content.Keys)
                {
                    await CreateMenuItem(viewModel, data.Content[key]);
                }
            }
            viewModel.Initialize(_configs.buttonConfigs);
            return viewModel;
        }

        private async Task<T> CreateItem<T>(string dateItemPath)
        {
            var prefab = await _assets.Instantiate(dateItemPath);
            return prefab.GetComponent<T>();
        }

        public void CleanUp()
        {
            _assets.Cleanup();
        }
    }
}