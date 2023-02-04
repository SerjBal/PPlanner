namespace SerjBal
{
    public interface IWindow
    {
        void SetEditFormatText(string inputFormat);
        void SetAcceptButtonText(string butoonText);
        void Initialize(IAppFactory appFactory, IViewModel menuItem);
    }
}