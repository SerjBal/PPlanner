using UnityEngine;

namespace SerjBal
{
    public class DataFormatCmd : ICommand
    {
        private readonly IWindowViewModel _viewModel;

        public DataFormatCmd(IWindowViewModel viewModel) => _viewModel = viewModel;

        public void Execute(object param = null)
        {
            if (param != null)
            {
                var value = int.Parse((string)param);
                _viewModel.InputString = Mathf.Clamp(value, 0, 12).ToString("D2");
            }
        }
    }
}