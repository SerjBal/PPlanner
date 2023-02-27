using System.Text.RegularExpressions;
using SerjBal.Windows;

namespace SerjBal
{
    public class NewPostWindow : NewItemWindow, IWindow
    {
        public override void Initialize(IMenuItem menuItem)
        {
            InputField.text = "00:00";
            inputField.onValueChanged.AddListener(CheckFormat);
            base.Initialize(menuItem);
        }

        public void CheckFormat(string value)
        {
            if (value.Contains(':'))
            {
                var split = value.Split(':');
                string hours = Regex.Replace(split[0], @"\D", "0");
                string minutes = Regex.Replace(split[1], @"\D", "0");
                inputField.text = $"{hours:00}:{minutes:00}";
            }
            else
            {
                inputField.text = "00:00";
            }
        }
    }
}

