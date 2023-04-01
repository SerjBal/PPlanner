namespace SerjBal
{
    public class ButtonExpandStartCmd : ICommand
    {
        private readonly ButtonViewModel _viewModel;

        public ButtonExpandStartCmd(ButtonViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Execute(object param = null)
        {
            _viewModel.ContentUpdateCommand?.Execute();
            _viewModel.ContentContainer.gameObject.SetActive(true);
            _viewModel.IsCanvasActive = true;
        }
    }
}