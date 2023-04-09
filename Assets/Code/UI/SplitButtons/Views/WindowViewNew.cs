using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SerjBal
{
    public class WindowViewNew : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI formatText;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button closeButton;
        public ICommand FormatCmd { get; set; }
        public ICommand CheckCmd { get; set; }
        public ICommand СonfirmCmd { get; set; }
        public ICommand AcceptCmd { get; set; }
        public string HeaderText { get; set; }
        public string AcceptButtonText { get; set; }
        public string InputString
        {
            get => _inputString;
            set
            {
                _inputString = value;
                inputField.text = value;
            }
        }

        public int SortingOrder
        {
            set => canvas.sortingOrder = value;
        }
        private string _inputString;

        public void Initialize(IWindowViewModel viewModel)
        {
            formatText.text = HeaderText;
            inputField.text = InputString;
            buttonText.text = AcceptButtonText;
            inputField.onValueChanged.AddListener(text => FormatCmd?.Execute(text));
            acceptButton.onClick.AddListener(() => CheckCmd?.Execute());
            closeButton.onClick.AddListener(Close);
        }

        private void Close()
        {
            Destroy(gameObject);
        }
    }
}