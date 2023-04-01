namespace SerjBal
{
    public interface ICommand
    {
        void Execute(object param = null);
    }
}