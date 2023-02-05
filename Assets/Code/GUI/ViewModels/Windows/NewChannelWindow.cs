using SerjBal.Windows;

namespace SerjBal
{
    public class NewChannelWindow : NewItemWindow, IWindow
    {
        public override void Initialize(IMenuItemViewModel menuItem)
        {
            base.Initialize(menuItem);
            InputField.text = "New Channel";
            onAccept += () => new Services().Single<IAppFactory>().CreateChannelItem(menuItem, InputField.text);
        }
    }
}