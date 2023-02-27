using SerjBal.Windows;

namespace SerjBal
{
    public class NewChannelWindow : NewItemWindow, IWindow
    {
        public override void Initialize(IMenuItem menuItem)
        {
            InputField.text = "New Channel";
            base.Initialize(menuItem);
        }
    }
}