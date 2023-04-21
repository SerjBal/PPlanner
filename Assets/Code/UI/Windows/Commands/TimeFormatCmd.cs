
using System.Text.RegularExpressions;
using TMPro;

namespace SerjBal
{
    public class TimeFormatCmd : ICommand
    {
        private readonly IWindowPresenter _presenter;
        private readonly TMP_InputField _input;
        
        private const string TimePattern = "^([0-1][0-9]|2[0-3]):([0-5][0-9])$"; 
        private string _defaultText = "00:00";

        public TimeFormatCmd(IWindowPresenter presenter, TMP_InputField tmpInputField)
        {
            _presenter = presenter;
            _input = tmpInputField;
            _presenter.InputString = Const.TimeDefaultKey;
        }

        public void Execute(object param = null)
        {
            if (param != null)
            {
                var value = (string)param;
                if (value.Length<5)
                {
                    Validate(_input);
                }
                _presenter.InputString = _input.text;
            }
        }

        public void Validate(TMP_InputField input)
        {
            int pos = input.caretPosition;
            var text = input.text;

            (_input.inputValidator as TimeValidator)?.Validate(ref text, ref pos, '\b');
            input.text = text;
            input.caretPosition = pos;
        }
    }
}