using SerjBal.Windows;

namespace SerjBal
{
    public class NewChannelWindow : EditWindow
    {
        public override void Initialize(IHierarchical splitButton, Services services, WindowView view)
        {
            CheckCmd = new ItemCheckNameCmd(this, splitButton.ContentPath);
            СonfirmCmd = new WarningWindowCreateCmd<ConfirmWindow>(this, services);
            AcceptCmd = new ButtonCreateCmd(this, services, splitButton);
            FormatCmd = new NameFormatCmd(this);

            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.NewChannelWindowFormatText;
            AcceptButtonText = Const.NewChannelWindowButtonText;
            
            InputString = Const.ChannelDefaultKey;
            InitializeView(view);
        }
    }
}