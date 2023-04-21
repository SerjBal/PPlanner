using System;
using SerjBal.Windows;

namespace SerjBal
{
    public abstract class EditWindow : IWindowPresenter
    {
        public ICommand FormatCmd { get; set; }
        public ICommand CheckCmd { get; set; }
        public ICommand СonfirmCmd { get; set; }
        public ICommand AcceptCmd { get; set; }

        private string _input;
        public string InputString
        {
            get => _input;
            set
            {
                _input = value;
                OnInputChanged?.Invoke(value);
            }
        }

        public string HeaderText { get; set; }
        public string AcceptButtonText { get; set; }

        public int SortingOrder
        { 
            set => OnCanvasChanged?.Invoke(value);
        }

        public Action<string> OnInputChanged { get; set; }
        public Action OnClose { get; set; }
        public Action<int> OnCanvasChanged { get; set; }

        public abstract void Initialize(IHierarchical splitButton, Services services,  WindowView view);
        
        private protected void InitializeView(WindowView view)
        {
            view.formatText.text = HeaderText;
            view.inputField.text = InputString;
            view.buttonText.text = AcceptButtonText;
            view.inputField.onValueChanged.AddListener(text => FormatCmd?.Execute(text));
            view.acceptButton.onClick.AddListener(() => CheckCmd?.Execute());
            view.closeButton.onClick.AddListener(() => UnityEngine.Object.Destroy(view.gameObject));
            OnInputChanged = value => view.inputField.text = value;
            OnCanvasChanged = value => view.canvas.sortingOrder = value;
            OnClose += () => UnityEngine.Object.Destroy(view.gameObject);
        }
    }
}