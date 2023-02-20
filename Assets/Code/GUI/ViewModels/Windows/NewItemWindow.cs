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
        protected string _currentKey;
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

        public async virtual void Accept()
        {
            var itemKey = _menuItem.GetKeyPath();
            string newKey = $"{itemKey}/{inputField.text}";
            ItemData keyData = new ItemData { Key = inputField.text, Content = new List<ItemData>() };
            IDataProvider data = _services.Single<IDataProvider>();
            bool hasData = data.HasKey(newKey);
            if (hasData)
            {
                var replaceWinow = await _services.Single<IWindowsFactory>().CreateReplacingDataWindow();
                replaceWinow.onAccept = () => OnAccept(keyData);
                replaceWinow.Initialize(_menuItem);
            }
            else
            {
                keyData = new ItemData { Key = inputField.text, Content = new List<ItemData>() };
                OnAccept(keyData);
            }
        }
        
        protected void OnAccept(ItemData newData)
        {
            onAccept?.Invoke();
            
            string newPath = $"{ _menuItem.GetKeyPath()}/{inputField.text}";
            _services.Single<ISaveLoad>().Save(newPath, newData);
            _menuItem.UpdateContent();
            Close();
        }
        
        public void SetHeaderText(string inputFormat) => formatText.text = inputFormat;
        public void SetAcceptButtonText(string button) => buttonText.text = button;
        protected void Close() => Destroy(gameObject);
    }
}