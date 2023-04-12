using System;
using System.Text;
using UnityEngine;

namespace SerjBal
{
    public class DataFormatCmd : ICommand
    {
        private readonly IWindowViewModel _viewModel;
        private string[] _defaultSplit;
        private readonly StringBuilder _stringBuilder;

        public DataFormatCmd(IWindowViewModel viewModel, Services services)
        {
            _viewModel = viewModel;
            var date = services.Single<IDataProvider>().CurrentDate;
            _defaultSplit = new[] { date.Year.ToString(), date.Month.ToString(), date.Day.ToString()};
            _stringBuilder = new StringBuilder();
        }

        public void Execute(object param = null)
        {
            if (param != null)
            {
                var value = (string)param;
                var str = _defaultSplit;
                var split = value.Split('-');
                for (int i = 0; i < str.Length; i++)
                    if (i < split.Length)
                    {
                        var charsCount = i == 0 ? 4 : 2;
                        str[i] = Check(split[i], charsCount);
                    }

                _viewModel.InputString = $"{str[0]}-{str[1]}-{str[2]}";
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