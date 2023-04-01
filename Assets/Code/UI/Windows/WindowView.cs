using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal.Windows
{
    public class WindowView : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI formatText;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button closeButton;

        public void Initialize(IWindowViewModel viewModel)
        {
            formatText.text = viewModel.HeaderText;
            inputField.text = viewModel.InputString;
            buttonText.text = viewModel.AcceptButtonText;
            inputField.onValueChanged.AddListener(text => viewModel.FormatCmd?.Execute(text));
            acceptButton.onClick.AddListener(() => viewModel.CheckCmd?.Execute());
            closeButton.onClick.AddListener(Close);
            viewModel.OnInputChanged += value => inputField.text = value;
            viewModel.OnCanvasChanged += value => canvas.sortingOrder = value;
            viewModel.OnClose += Close;
        }

        private void Close()
        {
            Destroy(gameObject);
        }
    }
}