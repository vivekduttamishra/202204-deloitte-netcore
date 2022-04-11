using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01
{
    public static class Middlewares
    {
        public static List<Stats> stats = new List<Stats>();
        public static List<string> wrongUrls = new List<string>();

        public static void UseOnUrl(this IApplicationBuilder app, string url, RequestDelegate action)
        {
            app.Use(next => async context =>
             {
                 if (context.Request.Path.Value == url && url != "/stats" && url != "/404")
                 {
                     AddCountOfUrl(context.Request.GetDisplayUrl());
                     await action(context);
                 }
                 else if (context.Request.Path.Value == "/stats")
                 {
                     ShowStats(context);
                 }
                 else if (context.Request.Path.Value == "/404")
                 {
                     ShowWrongUrlList(context);
                 }
                 else
                 {
                     await next(context);
                     if (context.Response.StatusCode == 404 && context.Request.Path.Value != "/")
                     {
                         if (!wrongUrls.Contains(context.Request.GetDisplayUrl()))
                         {
                             wrongUrls.Add(context.Request.GetDisplayUrl());
                         }
                     }
                 }
             });
        }

        private static void AddCountOfUrl(string url)
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

        private static void ShowStats(HttpContext context)
        {
            foreach (var item in stats)
            {
                context.Response.WriteAsync($"{item.url}:");
                context.Response.WriteAsync($"{item.count}\n");
            }
        }

        private static void ShowWrongUrlList(HttpContext context)
        {
            context.Response.WriteAsync(String.Join(" ", wrongUrls));
        }

    }
}
