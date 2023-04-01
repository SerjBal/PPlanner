namespace SerjBal
{
    public class ConfirmWindow : WarningWindow
    {
        public override void Initialize(IWindowViewModel window, string path)
        {
            CheckCmd = new ConfirmCmd(this, window, path);
            FormatCmd = new TimeFormatCmd(this);
            HeaderText = Const.ReplaceWarningText;
            AcceptButtonText = Const.ReplaceButtonText;
            SortingOrder = Const.WarningWindowSortingOrder;
        }
    }
}