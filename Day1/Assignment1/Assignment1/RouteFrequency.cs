using System.Collections.Generic;

namespace Assignment1
{
    public static class ValidRouteFrequency
    {
        public static Dictionary<string, int> log = new Dictionary<string, int>();
        public static Dictionary<string, int> GetLog()
        {
            return log;
        }

        public static void Add(string url)
        {
            if (log.ContainsKey(url))
                ++log[url];
            else
                log.Add(url, 1);
        }
    }

    public static class InvalidRouteFrequency
    {
        public static Dictionary<string, int> log = new Dictionary<string, int>();
        public static Dictionary<string, int> GetLog()
        {
            return log;
        }

        public static void Add(string url)
        {
            if (log.ContainsKey(url))
                ++log[url];
            else
                log.Add(url, 1);
        }
    }
}
