using SerjBal.Windows;

namespace SerjBal
{
    public class ConfirmWindow : WarningWindow
    {
        public override void Initialize(IWindowViewModel window, string path, WindowView view)
        {
            CheckCmd = new ConfirmCmd(this, window, path);
            FormatCmd = new TimeFormatCmd(this);
            HeaderText = Const.ReplaceWarningText;
            AcceptButtonText = Const.EditWindowButtonText;
            SortingOrder = Const.WarningWindowSortingOrder;
            
            InitializeView(view);
        }
    }
}