using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SerjBal.Windows
{
    public class NewItemWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI formatText;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button closeButton;
        protected UnityAction onAccept;
        protected TMP_InputField InputField => inputField;
        protected IMenuItem _menuItem;

        protected Services _services;
        
        public virtual void Initialize(IMenuItem menuItem)
        {
            //block menuItem Button
            _menuItem = menuItem;
            acceptButton.onClick.AddListener(Accept);
            closeButton.onClick.AddListener(Close);
            onAccept += OnAccept;
            _services = new Services();
        }
        public void SetEditFormatText(string inputFormat) => formatText.text = inputFormat;

        public void SetAcceptButtonText(string button) => buttonText.text = button;

        private void Close() => Destroy(gameObject);

        private void OnAccept()
        {
            _services.Single<ISaveLoad>().Save(_menuItem, inputField.text);
            Close();
        }

        private void Accept()
        {
            var dataProvider = _services.Single<IDataProvider>();
            bool hasData = dataProvider.DataHasKey(_menuItem, inputField.text);
            if (hasData)
            {
                _services.Single<IAppFactory>().CreateReplacingDataWindow(onAccept);
            }
            else
            {
                onAccept.Invoke();
            }
        }
    }
}