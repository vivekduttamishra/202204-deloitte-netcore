using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Service
{
    public class ShowStatsService : IDisplayTextService
    {
        public static List<Stats> stats = new List<Stats>();
        public static List<string> wrongUrls = new List<string>();

        public class StatsConfiguration
        {
            public string Prefix { get; set; }

            public string Suffix { get; set; }
        }

        private ILogger<ShowStatsService> logger;
        StatsConfiguration info;

        public ShowStatsService(IConfiguration config, ILogger<ShowStatsService> logger)
        {
            info = new StatsConfiguration();
            config.Bind("greeter", info);

            this.logger = logger;
            logger.LogInformation($"ConfigurableGreetService #{GetHashCode()} created with config: {info}");
        }

        public string DisplayText()
        {
            var count = info.Prefix;
            foreach (var item in stats)
            {
                count = count + $"{item.url}:{item.count}";
            }
            count = count + info.Suffix;
            return count;
        }

        public void AddStats(string url)
        {
            if (!stats.Any() || !stats.Any(x => x.url.Equals(url)))
            {
                var stat = new Stats
                {
                    url = url,
                    count = 1
                };
                stats.Add(stat);
            }
            else if (stats.Any(x => x.url.Equals(url)))
            {
                foreach (var item in stats)
                {
                    if (item.url == url)
                    {
                        item.count = item.count + 1;
                    }
                }
            }

        }

        public void Add404Urls(string url)
        {
            if (!wrongUrls.Contains(url))
            {
                wrongUrls.Add(url);
            }
        }

        public string ShowWrongUrlList()
        {
            var count = info.Prefix;
            count = count + string.Join(" ", wrongUrls);
            count = count + info.Suffix;
            return count;
        }
    }
}
