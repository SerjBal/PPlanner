

using System;
using System.Text;
using UnityEngine;

namespace SerjBal
{
    public class TimeFormatCmd : ICommand
    {
        private readonly IWindowViewModel _viewModel;

        public TimeFormatCmd(IWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.InputString = Const.TimeDefaultKey;
        }

        public void Execute(object param = null)
        {
            if (param != null)
            {
                var value = (string)param;
                _viewModel.InputString = value;
            }
        }
    }
}