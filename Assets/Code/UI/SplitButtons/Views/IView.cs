namespace SerjBal
{
    public interface IView
    {
        void Initialize(ButtonConfig config);
        void ReleaseSetup(ButtonViewModel viewModel);
    }
}