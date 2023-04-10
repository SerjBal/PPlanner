namespace SerjBal
{
    public class DateOverrideCmd : ButtonOverrideCmd
    {
        private readonly ICommand _editAcceptCmd;
        private readonly IGUI _GUI;


        public DateOverrideCmd(IWindowViewModel viewModel, Services services, IHierarchical itemViewModel)
            : base(viewModel, services, itemViewModel) => _GUI = services.Single<IGUI>();

        public override void Execute(object param = null)
        {
            base.Execute();
            _GUI.UpdateMenu();
        }
    }
}