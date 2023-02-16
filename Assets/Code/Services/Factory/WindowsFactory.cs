using System.Threading.Tasks;
using SerjBal.Code.Sources;
using UnityEngine;
using UnityEngine.Events;

namespace SerjBal
{
    public class WindowsFactory : IWindowsFactory
    {
        private IAssetsProvider _assets;

        public WindowsFactory(IAssetsProvider assets) => _assets = assets;

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(Const.EditWindowPath);
            await _assets.Load<GameObject>(Const.NewChannelWindowPath);
            await _assets.Load<GameObject>(Const.NewTimeWindowPath);
            await _assets.Load<GameObject>(Const.WarningWindowPath);
        }
        
        public async Task CreateEditDateWindow(IMenuItem menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.EditWindowPath, null);
            window.SetHeaderText(Const.EditDateWindowFormatText);
            window.SetAcceptButtonText(Const.EditDateWindowButtonText);
            window.Initialize(menuItem);
        }

        public async Task CreateEditChannelWindow(IMenuItem menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.EditWindowPath, null);
            window.SetHeaderText(Const.EditChannelWindowFormatText);
            window.SetAcceptButtonText(Const.EditChannelWindowButtonText);
            window.Initialize(menuItem);
        }
        
        public async Task CreateNewChannelWindow(IMenuItem menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.NewChannelWindowPath, null);
            window.SetHeaderText(Const.NewChannelWindowFormatText);
            window.SetAcceptButtonText(Const.NewChannelWindowButtonText);
            window.Initialize(menuItem);
        }

        public async Task CreateEditTimeWindow(IMenuItem menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.EditWindowPath, null);
            window.SetHeaderText(Const.EditTimeWindowFormatText);
            window.SetAcceptButtonText(Const.EditTimeWindowButtonText);
            window.Initialize(menuItem);
        }
        
        
        public async Task CreateNewTimeWindow(IMenuItem menuItem)
        {
            var window = await _assets.Instantiate<IWindow>(Const.NewTimeWindowPath, null);
            window.SetHeaderText(Const.NewTimeWindowFormatText);
            window.SetAcceptButtonText(Const.NewTimeWindowButtonText);
            window.Initialize(menuItem);
        }

        public async Task<IWarningWindow> CreateReplacingDataWindow()
        {
            var window = await _assets.Instantiate<IWarningWindow>(Const.WarningWindowPath, null);
            window.SetHeaderText(Const.ReplaceWarningText);
            window.SetAcceptButtonText(Const.ReplaceButtonText);
            return window;
        }

        public async Task<TextStyleLinkWindow> CreateTextLinkStyleWindow()
        {
            Debug.Log("link window");
            return null;
        }

        public Task<TextColorWindow> CreateTextColorWindow()
        {
            Debug.Log("color window");
            return null;
        }
    }
}