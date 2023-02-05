using System;
using UnityEngine.Events;

namespace SerjBal
{
    public interface IWarningWindow
    {
        void SetWarningText(string warningText);
        void SetAcceptButtonText(string buttonText);
        void Initialize(UnityAction OnAccept);
    }
}