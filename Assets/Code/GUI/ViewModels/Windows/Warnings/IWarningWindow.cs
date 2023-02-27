using UnityEngine.Events;

namespace SerjBal
{
    public interface IWarningWindow : IWindow
    {
        UnityAction onAccept { get; set; }
    }
}