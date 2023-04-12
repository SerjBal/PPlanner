using System.IO;

namespace SerjBal
{
    internal class TemplatesProvider : ITemplatesProvider
    {
        private readonly IDataProvider _data;

        public TemplatesProvider(IDataProvider data) => _data = data;

        public bool HasTamplates()
        {
            return false;
        }

        public void Create(string name)
        {
            var date = _data.CurrentDate;
            var dateName = $"{date.Year:D4}-{date.Month:D2}-{date.Day:D2}";
            var datePath = Path.Combine(Const.DataPath, dateName);
            var templatePath = Path.Combine(Const.DataPath, Const.TemplatesDirectory, Const.ContentDirectory, name);
            _data.DeleteDirectory(templatePath);
            _data.Copy(datePath, templatePath);
        }

        public void Load(string templatePath)
        {
            var date = _data.CurrentDate;
            var dateName = $"{date.Year:D4}-{date.Month:D2}-{date.Day:D2}";
            var datePath = Path.Combine(Const.DataPath, dateName);
            
            var subDir = _data.LoadDirectory(datePath);
            foreach (var data in subDir) 
                _data.DeleteDirectory(data);
            _data.Copy(templatePath, datePath);
        }
    }
}