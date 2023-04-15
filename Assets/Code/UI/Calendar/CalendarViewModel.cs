using System;
using SerjBal.Indication;

namespace SerjBal
{
    public class CalendarViewModel
    {
        private IDataProvider _data;
        private IGUI _GUI;
        private IPostIndication _indication;
        
        public void Initialize(CalendarView view, Services services)
        {
            _GUI = services.Single<IGUI>();
            _data = services.Single<IDataProvider>();
            _indication =  services.Single<IPostIndication>();

            view.OnLoadDate = LoadDate;
            view.OnDateCheck = IsDateExists;
            view.Initialize();
        }

        public bool IsDateExists(DateTime date)
        {
            _data.PathExists(date.ToPath());
            return false;
        }

        public void LoadDate(DateTime date)
        {
            _data.CurrentDate = date;
            _indication.Initialize(date);
            _GUI.UpdateMenu();
        }
    }
}