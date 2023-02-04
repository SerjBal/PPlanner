using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace SerjBal
{
    public interface IAppFactory : IService
    {
        Task WarmUp();
        Task CreateEditDateWindow(IViewModel menuItem);
        Task CreateNewChannelWindow(IViewModel menuItem);
        Task CreateNewPostWindow(IViewModel menuItem);
        Task<IViewModel> CreateDateItem(IViewModel parent);
        Task<IViewModel> CreateChannelItem(IViewModel parent, string channelKey);
        Task<IViewModel> CreateTimeItem(IViewModel parent, string timeKey);
        void CleanUp();
        Task CreateTextEditor(string chunnelID, string timeID);
        Task<MainView> CreateGUI();
        
    }
}