using System;
using SerjBal.Indication;

namespace SerjBal
{
    public class CalendarPresenter
    {
        private IDataProvider _data;
        private IGUI _GUI;
        private IPostIndicator _indicator;
        
        public void Initialize(CalendarView view, Services services)
        {
            _GUI = services.Single<IGUI>();
            _data = services.Single<IDataProvider>();
            _indicator =  services.Single<IPostIndicator>();

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
            _indicator.Initialize(date);
            _GUI.UpdateMenu();
        }
    }
}