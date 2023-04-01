namespace SerjBal
{
    public interface ITemplatesProvider : IService
    {
        string SelectedTemplatePath { get; set; }
        bool HasTamplates();
    }
}