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
            var itemKey = _menuItem.GetKeyPath();
            string newKey = $"{itemKey}/{InputField.text}";
            ItemData keyData;
            IDataProvider data = _services.Single<IDataProvider>();
            bool hasData = data.HasKey(newKey);
            if (hasData)
            {
                var replaceWinow = await _services.Single<IWindowsFactory>().CreateReplacingDataWindow();
                replaceWinow.onAccept = () => OnAccept();
                replaceWinow.Initialize(_menuItem);
            }
            else
            {
                data.RenameKey($"{itemKey}/{_currentKey}",InputField.text );
                _services.Single<ISaveLoad>().Save();
                _menuItem.UpdateContent();
            }
        }
        
        private void OnAccept()
        {
            IDataProvider data = _services.Single<IDataProvider>();
            var itemKey = _menuItem.GetKeyPath();
            data.RemoveKey($"{itemKey}/{InputField.text}");
            data.RenameKey($"{itemKey}/{_currentKey}",InputField.text );
            _services.Single<ISaveLoad>().Save();
            _menuItem.UpdateContent();
            Close();
        }
    }
}