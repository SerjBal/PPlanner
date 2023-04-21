namespace SerjBal
{
    public class DateCheckNameCmd : ItemCheckNameCmd
    {
        private EditDateWindow _viewModel;
        private readonly IDataProvider _data;

        public DateCheckNameCmd(IWindowPresenter presenter, string parentPath, Services services) : base(presenter, parentPath)
        {
            _viewModel = presenter as EditDateWindow;
            _data = services.Single<IDataProvider>();
        }

        private protected override string GetDateName()
        {
            var year = _data.CurrentDate.Year;
            var month = _viewModel.Month.ToString("D2");
            var day = _viewModel.InputString;
            return  $"{year}-{month}-{day}";
        }
    }
}