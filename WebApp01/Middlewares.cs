using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01
{
    public static class Middlewares
    {
       public static Dictionary<string, int> acceptedUrlStats = new Dictionary<string, int>();
        public static Dictionary<string, int> otherUrlStats = new Dictionary<string, int>();

        private const string stats = "/stats";
        public static bool acceptedUrl = false;

        public static void UseOnUrl(this IApplicationBuilder app, string url, RequestDelegate action)
        {
           
            app.Use(next => async context =>
            {
              
                if (context.Request.Path.Value == url)
                {
                    acceptedUrl = true;
                    if (context.Request.Path.Value != stats)
                    {
                        UpdateAcceptedUrlStats(url);
                    }

                     await action(context);
                    await next(context);
                }
                else
                    await next(context);
            });


        }

      public static void UpdateAcceptedUrlStats(string url)
        {
            if (acceptedUrlStats.ContainsKey(url))
            {
                acceptedUrlStats[url] = acceptedUrlStats[url] + 1;
            }
            else
            {
                
                acceptedUrlStats.Add(url, 1);
            }
        }

        public static void UpdateOtherUrlStats(string url)
        {
            if (otherUrlStats.ContainsKey(url))
            {
                otherUrlStats[url] = otherUrlStats[url] + 1;
            }
            else
            {

                otherUrlStats.Add(url, 1);
            }
        }


    }
}
