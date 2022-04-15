using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp01.Services
{
    public interface IUrlStatsService
    {
        Task AddNotFound(string url);
        Task AddVisitedUrl(string url);
        Task<List<string>> GetNotFoundUrls();
        Task<Dictionary<string, int>> GetStats();
    }
}