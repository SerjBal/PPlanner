using System.Threading.Tasks;

namespace SerjBal
{
    public interface IGUI : IService
    {
        Task Initialize(Services services);
        void UpdateMenu();
        void InteractionEnable(bool isTrue);
    }
}