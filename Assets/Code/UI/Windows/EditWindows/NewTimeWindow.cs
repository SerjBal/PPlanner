namespace SerjBal
{
    public class NewTimeWindow : EditWindow
    {
        public override void Initialize(IHierarchical splitButton, Services services)
        {
            CheckCmd = new ButtonCheckCmd(this, splitButton.Path);
            СonfirmCmd = new WarningWindowCreateCmd<ConfirmWindow>(this, services);
            AcceptCmd = new ButtonCreateCmd(this, services, splitButton);
            FormatCmd = new TimeFormatCmd(this);

            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.NewTimeWindowFormatText;
            AcceptButtonText = Const.NewTimeWindowButtonText;
            InputString = Const.TimeDefaultKey;
        }
    }
}