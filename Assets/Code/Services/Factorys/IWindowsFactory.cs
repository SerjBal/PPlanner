using System;
using System.Threading.Tasks;
using SerjBal.Windows;

namespace SerjBal
{
    public interface IWindowsFactory : IService
    {
        Task WarmUp();
        void CreateEditWindow<T>(IHierarchical viewModel, string addressable) where T : IWindowPresenter, new();
        void CreateWarningWindow<T>(IWindowPresenter window, string path) where T : IWindowPresenter, new();
        void CreateColorSelectorWindow(Action action, ColorSettingType settingType);
    }
}