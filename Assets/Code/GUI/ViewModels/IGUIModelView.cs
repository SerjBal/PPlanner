using System;
using System.Threading.Tasks;

namespace SerjBal
{
    public interface IGUIModelView : IService
    {
        Task Initialize(MainMenuItemView gui);
        float GetMenuBounds();
        void UpdateMenu();
        void InteractonEnable(bool isTrue);
    }
}