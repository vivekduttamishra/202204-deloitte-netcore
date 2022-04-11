using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWarePractice
{
    public class Startup
    {
        private Dictionary<string, int> validUrlVisitedCount = new Dictionary<string, int>();
        private Dictionary<string, int> invalidUrlVistedCount = new Dictionary<string, int>();

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        private void AddUrlCount(string url, ErrorType errorType)
        {
            if(errorType == ErrorType.Success)
            {
                AddCountToStats(validUrlVisitedCount, url);
            }
            else
            {
                AddCountToStats(invalidUrlVistedCount, url);
            }
        }

        private void AddCountToStats(Dictionary<string, int> statsUrls, string url)
        {
            if(statsUrls.ContainsKey(url)){
                statsUrls[url] = statsUrls[url] + 1;
            }
            else
            {
                statsUrls.Add(url, 1);
            }
        }

        private async Task WriteStats(HttpContext context, Dictionary<string, int> stats)
        {
            foreach (var url in stats)
            {
                await context.Response.WriteAsync($"Url:{url.Key} Visited Count {url.Value} <br>");
            }
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStats((url, err) => AddUrlCount(url, err));

            app.UseOnUrl("/books", async context =>
            {
                await context.Response.WriteAsync($"Book Details");
            });

            app.UseOnUrl("/authors", async context =>
            {
                await context.Response.WriteAsync($"author Details");
            });

            app.UseOnUrl("/stats", async context =>
            {
                await context.Response.WriteAsync("<h1>Valid URL's Visited</h1> <br>");
                await WriteStats(context,validUrlVisitedCount);

            });

            app.UseOnUrl("/404", async context =>
            {
               await context.Response.WriteAsync("<h1>Invalid URL's Visited</h1> <br>");
               await WriteStats(context ,invalidUrlVistedCount);
            });
        }
    }
}