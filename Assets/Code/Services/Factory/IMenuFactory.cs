using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public interface IMenuFactory : IService
    {
        Task WarmUp();
        Task<MainMenuViewModel> CreateMainMenu();
        Task<IHierarchical> CreateMenuButton(IHierarchical parent, string path);
        void CreateTemplatesButton(IHierarchical mainMenuView, string path);
        Task<IHierarchical> CreateTemplateButton(IHierarchical mainMenuView, string path);
        Task<IHierarchical> CreateSearchResultButton(IHierarchical parent, string path);
        Task<TextEditorViewModel> CreateTextEditor(IHierarchical parent, string path);
        Task<Button> CreateAddButton(Transform parent);
        Task<TButton> CreateButton<TButton>(string addressablePath, IHierarchical parent,
            string path) where TButton : ButtonViewModel, new();
    }
}