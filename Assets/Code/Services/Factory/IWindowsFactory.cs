using System.Threading.Tasks;
using SerjBal.Windows;

namespace SerjBal
{
    public interface IWindowsFactory : IService
    {
        Task WarmUp();
        void CreateEditWindow<T>(IHierarchical viewModel, string addressable) where T : IWindowViewModel, new();
        void CreateWarningWindow<T>(IWindowViewModel window, string path) where T : IWindowViewModel, new();
        Task<WindowView> CreateWindowView(string path);
    }
}