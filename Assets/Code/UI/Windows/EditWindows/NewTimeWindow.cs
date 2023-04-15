using SerjBal.Windows;

namespace SerjBal
{
    public class NewTimeWindow : EditTimeWindow
    {
        public override void Initialize(IHierarchical splitButton, Services services, WindowView view)
        {
            CheckCmd = new ButtonCheckNameCmd(this, splitButton.ContentPath);
            СonfirmCmd = new WarningWindowCreateCmd<ConfirmWindow>(this, services);
            AcceptCmd = new TimeCreateCmd(this, services, splitButton);
            FormatCmd = new TimeFormatCmd(this);

            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.NewTimeWindowFormatText;
            AcceptButtonText = Const.NewTimeWindowButtonText;
            
            InputString = Const.TimeDefaultKey;
            InitializeView(view);
        }
    }
}