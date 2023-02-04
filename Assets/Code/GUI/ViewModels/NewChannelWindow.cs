using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class NewChannelWindow : MonoBehaviour, IWindow
    {
        [SerializeField] private TextMeshProUGUI formatText;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button closeButton;
        private Action OnAccept;
        public void SetEditFormatText(string inputFormat)
        {
            formatText.text = inputFormat;
        }

        public void SetAcceptButtonText(string button)
        {
            buttonText.text = button;
        }

        public void Initialize(IAppFactory appFactory, IViewModel menuItem)
        {
            //block menuItem Button
            OnAccept = () => appFactory.CreateChannelItem(menuItem, formatText.text);
            inputField.text = "New Channel";
            acceptButton.onClick.AddListener(Accept);
            closeButton.onClick.AddListener(Close);
        }
        
        private void Close()
        {
            Destroy(gameObject);
        }

        private void Accept()
        {
            OnAccept.Invoke();
            Close();
        }
    }
}