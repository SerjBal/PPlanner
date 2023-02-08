
using SerjBal.Windows;

namespace SerjBal
{
    public class NewPostWindow : NewItemWindow, IWindow
    {
        public override void Initialize(IMenuItem menuItem)
        {
            base.Initialize(menuItem);
            InputField.text = "0:0";
            onAccept += () => new Services().Single<IAppFactory>().CreateTimeItem(menuItem, InputField.text);
        }
    }
}
