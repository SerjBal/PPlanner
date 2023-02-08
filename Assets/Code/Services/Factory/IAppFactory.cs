using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SerjBal
{
    public interface IAppFactory : IService
    {
        Task WarmUp();
        Task<MainMenuItemView> CreateGUI();
        Task CreateEditDateWindow(IMenuItem menuItem);
        Task CreateEditChannelWindow(IMenuItem channelMenuItem);
        Task CreateEditTimeWindow(IMenuItem channelMenuItem);
        Task CreateNewChannelWindow(IMenuItem menuItem);
        Task CreateNewTimeWindow(IMenuItem menuItem);
        Task CreateReplacingDataWindow(UnityAction onAccept);
        Task<IMenuItem> CreateDateItem();
        Task<IMenuItem> CreateChannelItem(IMenuItem parent, string channelKey);
        Task<IMenuItem> CreateTimeItem(IMenuItem parent, string timeKey);
        Task CreateTextEditor(IMenuItem parent, string textKey);
        Task<Button> CreateAddButton(Transform parent);
    }
}