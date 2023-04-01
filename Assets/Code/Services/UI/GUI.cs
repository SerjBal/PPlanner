using System.Threading.Tasks;
using UnityEngine;

namespace SerjBal
{
    public class GUI : IGUI
    {
        private IDataProvider _data;
        private MainMenuViewModel _mainMenuView;
        private IMenuFactory _menuFactory;

        public async Task Initialize(Services services)
        {
            _menuFactory = services.Single<IMenuFactory>();
            _data = services.Single<IDataProvider>();
            _mainMenuView = await _menuFactory.CreateMainMenu();
        }

        public void UpdateMenu()
        {
            if (_mainMenuView.ContentContainer.childCount > 0)
                Object.Destroy(_mainMenuView.ContentContainer.GetChild(0).gameObject);

            var path = _data.CurrentDate.ToPath();
            _menuFactory.CreateButton(_mainMenuView, path); //parent as none and date key
        }

        public void InteractionEnable(bool isTrue) => _mainMenuView.Block(isTrue);
    }
}