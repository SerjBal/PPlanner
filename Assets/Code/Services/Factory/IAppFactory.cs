using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace SerjBal
{
    public interface IAppFactory : IService
    {
        Task WarmUp();
        Task CreateEditDateWindow(IMenuItemViewModel menuItem);
        Task CreateEditChannelWindow(IMenuItemViewModel channelMenuItemViewModel);
        Task CreateEditTimeWindow(IMenuItemViewModel channelMenuItemViewModel);
        Task CreateNewChannelWindow(IMenuItemViewModel menuItem);
        Task CreateNewTimeWindow(IMenuItemViewModel menuItem);
        Task CreateReplacingDataWindow(UnityAction onAccept);
        Task<IMenuItemViewModel> CreateDateItem(IMenuItemViewModel parent);
        Task<IMenuItemViewModel> CreateChannelItem(IMenuItemViewModel parent, string channelKey);
        Task<IMenuItemViewModel> CreateTimeItem(IMenuItemViewModel parent, string timeKey);
        void CleanUp();
        Task CreateTextEditor(string chunnelID, string timeID);
        Task<MainMenuItemView> CreateGUI();

        
    }
}