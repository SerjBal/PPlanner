using System;
using SerjBal.Windows;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SerjBal
{
    public class ColorSelectorWindow : EditWindow
    {
        private ColorSelectorWindowView _selectorWindowView;
        private ColorSettingType _settingType;
        private ISettingsProvider _settings;
        private readonly Action _onAccept;
        private IGUI _gui;
        private Color _selectedColor;

        public ColorSelectorWindow(Action onAccept) => _onAccept = onAccept;

        public void Initialize(ColorSettingType settingType, Services services, WindowView view)
        {
            _gui = services.Single<IGUI>();
            _settings = services.Single<ISettingsProvider>();
            _settingType = settingType;
            _selectorWindowView = (ColorSelectorWindowView)view;
            _selectorWindowView.IndicatorColor = Color.white;
            _selectorWindowView.onColorSelected = SetColor;
            _selectorWindowView.acceptButton.onClick.AddListener(OnAccept);
            
            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.EditColorWindowFormatText;
            AcceptButtonText = Const.EditWindowButtonText;
            
            InitializeView(view);
        }
        public override void Initialize(IHierarchical splitButton, Services services,  WindowView view) { }

        private void SetColor(Color color) => _selectedColor = color;

        private void OnAccept()
        {
            _settings.SaveColorSettings(_settingType, _selectedColor);
            _onAccept?.Invoke();
            _gui.UpdateMenu();
            OnClose.Invoke();
        }
    }
}