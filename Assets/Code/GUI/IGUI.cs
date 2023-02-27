using System.Threading.Tasks;

namespace SerjBal
{
    public interface IGUI : IService
    {
        Task Initialize(MainMenuItemView gui);
        void UpdateMenu();
        void InteractonEnable(bool isTrue);
    }
}