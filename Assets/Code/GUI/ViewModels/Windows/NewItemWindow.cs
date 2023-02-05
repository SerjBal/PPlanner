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
        public UnityAction onAccept;
        public TMP_InputField InputField => inputField;
        private IMenuItemViewModel _menuItem;
        
        public void SetEditFormatText(string inputFormat)
        {
            formatText.text = inputFormat;
        }

        public void SetAcceptButtonText(string button)
        {
            buttonText.text = button;
        }

        public virtual void Initialize(IMenuItemViewModel menuItem)
        {
            //block menuItem Button
            _menuItem = menuItem;
            acceptButton.onClick.AddListener(Accept);
            closeButton.onClick.AddListener(Close);
            onAccept += OnAccept;
        }
        
        private void Close()
        {
            Destroy(gameObject);
        }

        private void OnAccept()
        {
            new Services().Single<ISaveLoad>().Save();
            Close();
        }

        private void Accept()
        {
            var dataProvider = new Services().Single<IDataProvider>();
            bool hasData = dataProvider.DataHasKey(_menuItem.Key, inputField.text);
            if (hasData)
            {
                new Services().Single<IAppFactory>().CreateReplacingDataWindow(onAccept);
            }
            else
            {
                onAccept.Invoke();
            }
        }
    }
}