using System;

namespace SerjBal
{
    public class CalendarViewModel
    {
        private readonly IDataProvider _data;
        private readonly IGUI _GUI;

        public CalendarViewModel(Services services)
        {
            _GUI = services.Single<IGUI>();
            _data = services.Single<IDataProvider>();
        }

        public bool IsDateExists(DateTime date)
        {
            _data.PathExists(date.ToPath());
            return false;
        }

        public void LoadDate(DateTime date)
        {
            _data.CurrentDate = date;
            _GUI.UpdateMenu();
        }
    }
}