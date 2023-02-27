using System;
using System.Collections.Generic;
using SerjBal.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class EditWindow : NewItemWindow, IWindow
    {
        public override void Initialize(IMenuItem menuItem)
        {
            InputField.text = menuItem.Key;
            base.Initialize(menuItem.Parent);
        }
        
        public async override void Accept()
        {
            string itemKey = _menuItem.GetKeyPath();
            string newKey = $"{itemKey}/{InputField.text}";
            if (_data.HasKey(newKey))
            {
                var replaceWinow = await _services.Single<IWindowsFactory>().CreateReplacingDataWindow();
                replaceWinow.onAccept = () => OnAccept();
                replaceWinow.Initialize(_menuItem);
            }
            else
            {
                _data.RenameKey($"{itemKey}/{_currentKey}",InputField.text );
                _services.Single<ISaveLoad>().Save();
                _menuItem.UpdateContent();
            }
        }
        
        private void OnAccept()
        {
            string itemKey = _menuItem.GetKeyPath();
            _data.RemoveKey($"{itemKey}/{InputField.text}");
            _data.RenameKey($"{itemKey}/{_currentKey}",InputField.text );
            _saveLoad.Save();
            _menuItem.UpdateContent();
            Close();
        }
    }
}