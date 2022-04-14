using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp01.Services
{
    public interface IUrlService
    {
        Task StoreUrls(string url);
        Task<Dictionary<string, int>> GetStats();

        Task StoreUnHandledUrls(string url);

        Task<Dictionary<string, int>> GetUnHandledUrls();
    }
}
