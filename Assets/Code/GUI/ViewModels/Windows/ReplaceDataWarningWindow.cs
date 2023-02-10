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
        public void Initialize(UnityAction onAccept)
        {
            canvas.sortingOrder = Const.WarningWindowSortingOrder;
            //add on accept override data method by delete compared item and save new by Save(IMenuItem, Key)
            acceptButton.onClick.AddListener(onAccept);
            closeButton.onClick.AddListener(Close);
        }

        public void SetWarningText(string warning) => warningText.text = warning;
        public void SetAcceptButtonText(string button) => buttonText.text = button;
        private void Close() => Destroy(gameObject);
    }
}