using System;
using System.Threading.Tasks;
using SerjBal.Code.Sources;
using UnityEngine;

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

        public async Task Initialize()
        {
            _GUI = await _factory.CreateGUI();
            await _factory.CreateDateItem();
        }

        public CanvasGroup GetCanvasGroup() => _GUI.canvasGroup;


        public float GetMenuBounds()
        {
            return _GUI.lowScreenContainer.rect.height;
        }
        public void SaveTemnplate()
        {
            
        }
    }
}