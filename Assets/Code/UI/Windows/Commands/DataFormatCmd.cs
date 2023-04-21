using UnityEngine;

namespace SerjBal
{
    public class DataFormatCmd : ICommand
    {
        private readonly IWindowPresenter _presenter;

        public DataFormatCmd(IWindowPresenter presenter) => _presenter = presenter;

        public void Execute(object param = null)
        {
            if (param != null)
            {
                var value = int.Parse((string)param);
                _presenter.InputString = Mathf.Clamp(value, 0, 12).ToString("D2");
            }
        }
    }
}