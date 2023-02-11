using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SerjBal
{
    public class GUIModelView : IGUIModelView
    {
        private readonly IAppFactory _factory;
        private readonly ISaveLoad _saveLoad;
        private MainMenuItemView _GUI;

        public GUIModelView(IAppFactory factory, ISaveLoad saveLoad)
        {
            _factory = factory;
            _saveLoad = saveLoad;
        }

        public async Task Initialize(MainMenuItemView gui)
        {
            _GUI = gui;
            await _factory.CreateDateItem();
        }

        public float GetMenuBounds() => _GUI.dateContainer.rect.height;

        public void UpdateMenu()
        {
            var date = _GUI.dateContainer.GetChild(0).GetComponent<DateMenuItem>();
            foreach (Transform item in date.ContentContainer) { Object.Destroy(item.gameObject);}
            date.ShowContent();
        }

        public void DisableMenuInteracton(bool isTrue) => _GUI.canvasGroup.interactable = isTrue;
        
    }
}