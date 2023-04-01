using System.Threading.Tasks;

namespace SerjBal
{
    public interface IWindowsFactory : IService
    {
        Task WarmUp();
        void CreateEditWindow<T>(IHierarchical viewModel) where T : IWindowViewModel, new();
        void CreateWarningWindow<T>(IWindowViewModel window, string path) where T : IWindowViewModel, new();
    }
}