using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SerjBal
{
    public interface IMenuFactory : IService
    {
        Task WarmUp();
        Task<MainMenuItemView> CreateGUI();
        Task<IMenuItem> CreateDateItem();
        Task<IMenuItem> CreateChannelItem(IMenuItem parent, string channelKey);
        Task<IMenuItem> CreateTimeItem(IMenuItem parent, string timeKey);
        Task CreateTextEditor(IMenuItem parent, string textKey);
        Task<Button> CreateAddButton(Transform parent);
    }
}