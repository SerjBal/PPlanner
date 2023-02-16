using System;
using UnityEngine.Events;

namespace SerjBal
{
    public interface IWarningWindow : IWindow
    {
        string currentKey { get; set; }
        UnityAction onAccept { get; set; }
    }
}