using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp01.Services
{
    public class StatsService : IUrlService
    {
        private readonly Dictionary<string, int> dictionary;
        private readonly Dictionary<string, int> unhandledUrls;
        public StatsService()
        {
            this.dictionary = new Dictionary<string, int>();
            this.unhandledUrls = new Dictionary<string, int>();
        }
        public async Task StoreUrls(string url)
        {
            if (dictionary.ContainsKey(url))
            {
                dictionary[url] += 1;
            }
            else
            {
                dictionary.Add(url, 1);
            }
        }

        public async Task StoreUnHandledUrls(string url)
        {
            if (unhandledUrls.ContainsKey(url))
            {
                unhandledUrls[url] += 1;
            }
            else
            {
                unhandledUrls.Add(url, 1);
            }
        }

        public async Task<Dictionary<string, int>> GetUnHandledUrls()
        {
            return unhandledUrls;
        }

        public async Task<Dictionary<string, int>> GetStats()
        {
            return dictionary;
        }
    }
}
