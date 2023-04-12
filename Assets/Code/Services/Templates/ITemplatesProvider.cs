namespace SerjBal
{
    public interface ITemplatesProvider : IService
    {
        bool HasTamplates();
        void Create(string name);

        void Load(string name);
    }
}