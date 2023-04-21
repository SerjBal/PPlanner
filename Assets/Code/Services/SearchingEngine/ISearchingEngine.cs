using System.Collections.Generic;
using System.Threading.Tasks;

namespace SerjBal.Searching
{
    public interface ISearchingEngine : IService
    {
        Task<bool> Search(string goal, int daysRange = 0);
        Dictionary<string, PostData> GetResults();
    }
}