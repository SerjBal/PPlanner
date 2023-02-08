namespace SerjBal
{
    public interface ITemplatesProvider : IService
    {
        bool HasTamplates();
        ItemData SelectedTemplate { get; set; }
    }
}