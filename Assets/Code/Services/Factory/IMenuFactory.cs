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
        Task<TextEditorViewModel> CreateTextEditor(IHierarchical parent, string path);
        Task<Button> CreateAddButtonItem(Transform parent);
    }
}