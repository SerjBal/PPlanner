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
            var split = value.Split(':');
            string hours = Regex.Replace(split[0], @"\D", "0");
            string minutes = Regex.Replace(split[1], @"\D", "0");
            inputField.text = string.Format("{0:00}:{1:00}", hours, minutes);
        }
    }
}

