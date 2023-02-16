using System;
using SerjBal.Code.Sources;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SerjBal.Windows
{
    public class ReplaceDataWarningWindow : MonoBehaviour, IWarningWindow
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI warningText;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button closeButton;
        public UnityAction onAccept { get; set; }
        public IMenuItem menuItem { get; set; }
        public string currentKey { get; set; }
        public void Initialize(IMenuItem item)
        {
            menuItem = item;
            canvas.sortingOrder = Const.WarningWindowSortingOrder;
            acceptButton.onClick.AddListener(OnAccept);
            closeButton.onClick.AddListener(Close);
        }

        private void OnAccept()
        {
            for (int i = 0; i < menuItem.Childs.Count; i++)
            {
                var item = menuItem.Childs[i];
                if (item.Key == currentKey) item.Remove();
            }
            onAccept.Invoke();
            Close();
        }
        
        public void SetHeaderText(string warning) => warningText.text = warning;
        public void SetAcceptButtonText(string button) => buttonText.text = button;
        private void Close() => Destroy(gameObject);
    }
}