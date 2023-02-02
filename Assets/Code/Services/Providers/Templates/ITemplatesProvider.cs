namespace SerjBal
{
    public interface ITemplatesProvider : IService
    {
        bool HasTamplates();
        DateData SelectedTemplate { get; set; }
    }
}