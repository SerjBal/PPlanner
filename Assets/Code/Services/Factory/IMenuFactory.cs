using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public interface IMenuFactory : IService
    {
        Task WarmUp();
        Task<MainMenuViewModel> CreateMainMenu();
        Task<IHierarchical> CreateButton(IHierarchical parent, string path);
        Task<TButton> CreateButton<TButton>(string addressablePath, IHierarchical parent,
            string path) where TButton : ButtonViewModel, new();
        Task<TextEditorViewModel> CreateTextEditor(IHierarchical parent, string path);
        Task<Button> CreateAddButton(Transform parent);
    }
}