using SerjBal.Windows;

namespace SerjBal
{
    public class NewTemplateWindow : EditWindow
    {
        public override void Initialize(IHierarchical splitButton, Services services, WindowView view)
        {
            CheckCmd = new ButtonCheckNameCmd(this, splitButton.ContentPath);
            СonfirmCmd = new WarningWindowCreateCmd<ConfirmWindow>(this, services);
            AcceptCmd = new TemplateCreateCmd(this, services, splitButton);
            FormatCmd = new NameFormatCmd(this);

            SortingOrder = Const.MenuWindowSortingOrder;
            HeaderText = Const.NewTemplateWindowFormatText;
            AcceptButtonText = Const.NewTimeWindowButtonText;
            
            InputString = Const.DefaultTemplateName;
            InitializeView(view);
        }
    }
}