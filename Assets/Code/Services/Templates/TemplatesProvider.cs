namespace SerjBal
{
    internal class TemplatesProvider : ITemplatesProvider
    {
        public bool HasTamplates()
        {
            return false;
        }

        public string SelectedTemplatePath { get; set; }
    }
}