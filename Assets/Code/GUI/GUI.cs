using System.Threading.Tasks;

namespace SerjBal
{
    public class GUI : IGUI
    {
        private MainMenuItemView _GUI;

        public async Task Initialize(MainMenuItemView gui) => _GUI = gui;

        public void UpdateMenu() => _GUI.UpdateMenu();

        public void InteractonEnable(bool isTrue) => _GUI.blocker.SetActive(isTrue);
    }
}