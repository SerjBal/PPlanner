using System;

namespace SerjBal
{
    public abstract class WarningWindow : IWindowViewModel
    {
        private string _inputString;
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

        public Action OnClose { get; set; }
        public Action<int> OnCanvasChanged { get; set; }
        public Action<string> OnInputChanged { get; set; }

        public abstract void Initialize(IWindowViewModel window, string path);
    }
}