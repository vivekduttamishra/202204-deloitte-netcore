using MiddleWarePractice.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace MiddleWarePractice.Services
{
    public class StatsService : IStatsService
    {
        private Dictionary<string, int> validUrlVisitedCount = new Dictionary<string, int>();
        private Dictionary<string, int> invalidUrlVistedCount = new Dictionary<string, int>();

        public void AddValidUrlCount(string url)
        {
            AddCountToStats(validUrlVisitedCount, url);
        }
        public void AddInvalidUrlCount(string url)
        {
            AddCountToStats(invalidUrlVistedCount, url);

        }

        private void AddCountToStats(Dictionary<string, int> statsUrls, string url)
        {
            if (statsUrls.ContainsKey(url))
            {
                statsUrls[url] = statsUrls[url] + 1;
            }
            else
            {
                statsUrls.Add(url, 1);
            }
        }

        private string GetStats(Dictionary<string, int> urls)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var url in urls)
            {
                stringBuilder.Append($"Url:{url.Key} Visited Count {url.Value} <br>");
            }
            return stringBuilder.ToString();
        }

        public string GetValidUrlStats()
        {
            return GetStats(validUrlVisitedCount);
        }
        public string GetInvalidUrlStats()
        {
            return GetStats(invalidUrlVistedCount);
        }
    }
}
