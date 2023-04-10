using System.IO;

namespace SerjBal
{
    public class EditChannelWindow : EditWindow
    {
        public override void Initialize(IHierarchical splitButton, Services services)
        {
            CheckCmd = new ButtonCheckCmd(this, splitButton.Parent.ContentPath);
            СonfirmCmd = new WarningWindowCreateCmd<ConfirmWindow>(this, services);
            AcceptCmd = new ButtonOverrideCmd(this, services, splitButton);
            FormatCmd = new ChannelFormatCmd(this);

            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.EditChannelWindowFormatText;
            AcceptButtonText = Const.EditChannelWindowButtonText;
            InputString = Path.GetFileName(splitButton.Path);
        }
    }
}