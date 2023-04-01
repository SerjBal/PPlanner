namespace SerjBal
{
    public class DateOverrideCmdAdapter : ICommand
    {
        private readonly ICommand _editAcceptCmd;
        private readonly IGUI _GUI;

        public DateOverrideCmdAdapter(ButtonOverrideCmd editAcceptCmd, Services services)
        {
            _editAcceptCmd = editAcceptCmd;
            _GUI = services.Single<IGUI>();
        }

        public void Execute(object param = null)
        {
            _editAcceptCmd.Execute();
            _GUI.UpdateMenu();
        }
    }
}