using System;
using System.Collections.Generic;
using SerjBal.Code.Sources;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SerjBal.Windows
{
    public class NewItemWindow : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI formatText;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button closeButton;
        private string _currentKey;
        protected UnityAction onAccept;
        protected TMP_InputField InputField => inputField;
        protected IMenuItem _menuItem;
        protected Services _services;
        
        public virtual void Initialize(IMenuItem menuItem)
        {
            Bind();
            canvas.sortingOrder = Const.MenuWindowSortingOrder;
            _currentKey = inputField.text;
            _menuItem = menuItem;
            _services = new Services();
        }

        private void Bind()
        {
           acceptButton.onClick.AddListener(Accept);
           closeButton.onClick.AddListener(Close);
        }

        private async void Accept()
        {
            string key = inputField.text;
            ItemData keyData;
            var data = _services.Single<IDataProvider>();
            bool hasData = data.DataHasKey(_menuItem, key);
            if (hasData)
            {
                keyData = data.GetDataOf(_menuItem).Get(_currentKey);

                var replaceWinow = await _services.Single<IWindowsFactory>().CreateReplacingDataWindow();
                replaceWinow.currentKey = _currentKey;
                replaceWinow.onAccept = () => OnAccept(keyData);
                replaceWinow.Initialize(_menuItem);
            }
            else
            {
                keyData = new ItemData { Key = key, Content = new List<ItemData>() };
                OnAccept(keyData);
            }
        }
        
        private void OnAccept(ItemData keyData)
        {
            keyData.Key = inputField.text;
            _services.Single<ISaveLoad>().Save(_menuItem, inputField.text, keyData);
            onAccept.Invoke();
            Close();
        }
        
        public void SetHeaderText(string inputFormat) => formatText.text = inputFormat;

        public void SetAcceptButtonText(string button) => buttonText.text = button;
        private void Close() => Destroy(gameObject);
    }
}