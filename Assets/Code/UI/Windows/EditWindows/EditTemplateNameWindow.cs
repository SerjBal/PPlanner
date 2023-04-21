using System.IO;
using SerjBal.Windows;

namespace SerjBal
{
    public class EditTemplateNameWindow : EditWindow
    {
        public override void Initialize(IHierarchical splitButton, Services services, WindowView view)
        {
            CheckCmd = new ItemCheckNameCmd(this, splitButton.Path);
            СonfirmCmd = new WarningWindowCreateCmd<ConfirmWindow>(this, services);
            AcceptCmd = new ButtonOverrideCmd(this, services, splitButton);
            FormatCmd = new NameFormatCmd(this);

            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.EditTemplateWindowFormatText;
            AcceptButtonText = Const.EditWindowButtonText;
            
            InputString = Path.GetFileName(splitButton.Path);
            InitializeView(view);
        }
    }
}