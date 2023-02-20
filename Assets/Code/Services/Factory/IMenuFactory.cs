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

        Task<IMenuItem> CreateMenuItem(IMenuItem parent, string channelKey);
        Task<IMenuItem> CreateDateItem();
        Task CreateTextEditor(IMenuItem parent, string textKey);
        Task<Button> CreateAddButton(Transform parent);
    }
}