using System.Threading.Tasks;
using UnityEngine.Events;

namespace SerjBal
{
    public interface IWindowsFactory : IService
    {
        Task WarmUp();
        Task CreateEditDateWindow(IMenuItem menuItem);
        Task CreateEditChannelWindow(IMenuItem channelMenuItem);
        Task CreateEditTimeWindow(IMenuItem channelMenuItem);
        Task CreateNewChannelWindow(IMenuItem menuItem);
        Task CreateNewTimeWindow(IMenuItem menuItem);
        Task<IWarningWindow>CreateReplacingDataWindow();
        Task<TextStyleLinkWindow> CreateTextLinkStyleWindow(TextStyleEditor textStyleEditor);
        Task<TextColorWindow> CreateTextColorWindow(TextStyleEditor textStyleEditor);
    }
}