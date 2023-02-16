namespace SerjBal
{
    public interface IWindow
    {
        void SetHeaderText(string inputFormat);
        void SetAcceptButtonText(string butoonText);
        void Initialize(IMenuItem menuItem);
    }
}