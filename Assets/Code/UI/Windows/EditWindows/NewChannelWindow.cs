namespace SerjBal
{
    public class NewChannelWindow : EditWindow
    {
        public override void Initialize(IHierarchical splitButton, Services services)
        {
            CheckCmd = new ButtonCheckNameCmd(this, splitButton.ContentPath);
            СonfirmCmd = new WarningWindowCreateCmd<ConfirmWindow>(this, services);
            AcceptCmd = new ButtonCreateCmd(this, services, splitButton);
            FormatCmd = new NameFormatCmd(this);

            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.NewChannelWindowFormatText;
            AcceptButtonText = Const.NewChannelWindowButtonText;
            InputString = Const.ChannelDefaultKey;
        }
    }
}