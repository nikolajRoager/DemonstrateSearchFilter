using DemonstrateSearchFilter.Models;

namespace DemonstrateSearchFilter.Services
{
    public interface IBoxService
    {
        Task<IEnumerable<Box>> GetBoxesByKeyValuesAsync(Dictionary<string,string> query);
    }
}
