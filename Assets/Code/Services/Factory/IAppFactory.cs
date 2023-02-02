using System.Threading.Tasks;
using UnityEngine;

namespace SerjBal
{
    public interface IAppFactory : IService
    {
        Task WarmUp();
        Task<IViewModel> CreateDateItem(IViewModel parent);
        Task<IViewModel> CreateChannelItem(IViewModel parent);
        Task<IViewModel> CreateTimeItem(IViewModel parent);
        void CleanUp();
        Task CreateTextEditor(string chunnelID, string timeID);
        Task<MainView> CreateGUI();
    }
}