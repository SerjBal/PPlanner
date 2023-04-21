using System;
using System.IO;

namespace SerjBal
{
    public class DateOverrideCmd : ButtonOverrideCmd
    {
        private readonly ICommand _editAcceptCmd;
        private readonly IGUI _GUI;
        private EditDateWindow _viewModel;


        public DateOverrideCmd(IWindowPresenter presenter, Services services, IHierarchical itemViewModel)
            : base(presenter, services, itemViewModel) => _GUI = services.Single<IGUI>();

        public override void Execute(object param = null)
        {
            _viewModel = presenter as EditDateWindow; 
            base.Execute();
            _GUI.UpdateMenu();
        }
        
        private protected override string GetNewPath()
        {
            var year = data.CurrentDate.Year;
            var month = _viewModel.Month;
            var day = int.Parse(_viewModel.InputString);
           
            data.CurrentDate = new DateTime(year, month, day);
            
            var name = $"{year}-{month:D2}-{day}";
            var root = Directory.GetParent(itemViewModel.Path);
            var path = Path.Combine(root.FullName, name);
            return path;
        }
    }
}