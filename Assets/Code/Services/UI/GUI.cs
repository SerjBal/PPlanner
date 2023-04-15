using System.IO;
using System.Threading.Tasks;

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
                _mainMenuView.ContentContainer.Clear();

            var path = _data.CurrentDate.ToPath();
            _menuFactory.CreateMenuButton(_mainMenuView, path);
            _menuFactory.CreateTemplatesButton(_mainMenuView, Path.Combine(Const.DataPath, Const.TemplatesButtonName));
        }

        public void InteractionEnable(bool isTrue) => _mainMenuView.Block(isTrue);
    }
}