using System;
using System.Threading.Tasks;

namespace SerjBal
{
    public interface IGUIModelView : IService
    {
        Task Initialize();
        float GetMenuBounds();
        void UpdateMenu();
        void DisableMenuInteracton(bool p0);
    }
}