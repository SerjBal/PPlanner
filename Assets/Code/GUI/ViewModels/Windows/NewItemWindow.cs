using System;
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
        protected UnityAction onAccept;
        protected TMP_InputField InputField => inputField;
        protected IMenuItem _menuItem;
        protected Services _services;
        
        public virtual void Initialize(IMenuItem menuItem)
        {
            canvas.sortingOrder = Const.MenuWindowSortingOrder;
            _menuItem = menuItem;
            _services = new Services();
            Bind();
        }

        private void Bind()
        {
            acceptButton.onClick.AddListener(Accept);
            closeButton.onClick.AddListener(Close);
            onAccept += OnAccept;
        }

        private void Accept()
        {
            var data = _services.Single<IDataProvider>();
            bool hasData = data.DataHasKey(_menuItem, inputField.text);
            if (hasData)
            {
                _services.Single<IAppFactory>().CreateReplacingDataWindow(onAccept);
            }
            else
            {
                onAccept.Invoke();
            }
        }
        
        private void OnAccept()
        {
            _services.Single<ISaveLoad>().Save(_menuItem, inputField.text);
            Close();
        }
        
        public void SetEditFormatText(string inputFormat) => formatText.text = inputFormat;
        public void SetAcceptButtonText(string button) => buttonText.text = button;
        private void Close() => Destroy(gameObject);
    }
}