

using System;
using System.Text;
using UnityEngine;

namespace SerjBal
{
    public class TimeFormatCmd : ICommand
    {
        private readonly IWindowViewModel _viewModel;
        private string[] _defaultSplit;
        private readonly StringBuilder _stringBuilder;

        public TimeFormatCmd(IWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.InputString = Const.TimeDefaultKey;
            _defaultSplit = new string[]{"00","00"};
            _stringBuilder = new StringBuilder();
        }

        public void Execute(object param = null)
        {
            if (param != null)
            {
                var value = (string)param;
                var str = _defaultSplit;
                var split = value.Split(':');
                for (int i = 0; i < str.Length; i++)
                    if (i < split.Length)
                        str[i] = Check(split[i], 2);

                var hours = Mathf.Clamp(int.Parse(str[0]), 0, 23);
                var minuts = Mathf.Clamp(int.Parse(str[1]), 0, 59);
               // var minuts = Mathf.Clamp(Convert.ToInt32(str[1][0]), 0, 5);
                
                _viewModel.InputString = $"{hours:D2}:{minuts:D2]}";
                _defaultSplit = str;
            }
        }

        private string Check(string input, int max)
        {
            _stringBuilder.Clear();
            int i = 1;
            foreach (char c in input)
            {
                if (Char.IsDigit(c))
                    _stringBuilder.Append(c);
                else
                    _stringBuilder.Append('0');

                if (i == max)
                    break;
                else
                    i++;
            }

            return _stringBuilder.ToString();
        }
    }
}