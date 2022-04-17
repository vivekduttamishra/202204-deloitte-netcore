using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Services
{
    public class InMemoryUrlStatsService : IUrlStatsService
    {
        Dictionary<String, int> urls = new Dictionary<string, int>();
        List<string> urls404 = new List<string>();

        public async Task AddVisitedUrl(string url)
        {
            await Task.Yield();
            if (urls.ContainsKey(url))
                urls[url]++;
            else
                urls[url] = 1;
        }

        public async Task AddNotFound(string url)
        {
            await Task.Yield();
            urls404.Add(url);
        }

        public async Task<Dictionary<string, int>> GetStats()
        {
            await Task.Yield();
            return urls;
        }

        public async Task<List<string>> GetNotFoundUrls()
        {
            await Task.Yield();
            return urls404;
        }


    }
}
