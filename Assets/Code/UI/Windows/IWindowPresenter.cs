using System;
using TMPro;

namespace SerjBal
{
    public interface IWindowPresenter
    {
        public string InputString { get; set; }
        string HeaderText { get; set; }
        string AcceptButtonText { get; set; }
        int SortingOrder { set; }
        ICommand FormatCmd { get; set; }
        ICommand CheckCmd { get; set; }
        ICommand СonfirmCmd { get; set; }
        ICommand AcceptCmd { get; set; }
        Action<string> OnInputChanged { get; set; }
        Action OnClose { get; set; }
        Action<int> OnCanvasChanged { get; set; }
    }
}