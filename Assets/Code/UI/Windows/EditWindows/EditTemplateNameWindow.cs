using System.IO;

namespace SerjBal
{
    public class EditTemplateNameWindow : EditWindow
    {
        public override void Initialize(IHierarchical splitButton, Services services)
        {
            CheckCmd = new ButtonCheckNameCmd(this, splitButton.Path);
            СonfirmCmd = new WarningWindowCreateCmd<ConfirmWindow>(this, services);
            AcceptCmd = new ButtonOverrideCmd(this, services, splitButton);
            FormatCmd = new NameFormatCmd(this);

            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.EditTemplateWindowFormatText;
            AcceptButtonText = Const.EditWindowButtonText;
            InputString = Path.GetFileName(splitButton.Path);
        }
    }
}