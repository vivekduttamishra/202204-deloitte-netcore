using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp01
{
    public class Startup
    {
        Dictionary<string, int> keyValues = new Dictionary<string, int>();
        Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DateTime dateTime = DateTime.Now;
            app.Use(next => async context =>
            {
                await CaptureUrl(context.Request.Path.Value, keyValues);
                await next(context);
            });

            app.UseOnUrl("/Hello", async context =>
            {
                await context.Response.WriteAsync($"{context.Request.Path.Value}");
            });

            app.UseOnUrl("/long-running", async context =>
            {

                await context.Response.WriteAsync("Long running The work started...");
                await Task.Delay(2000);
                await context.Response.WriteAsync("Long running workd finished...");

            });

            app.UseOnUrl("/date", async context =>
            {
                await context.Response.WriteAsync($"Date is : {DateTime.Now.ToLongDateString()}");
            });

            app.UseOnUrl("/time", async context =>
            {
                await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

            });

            app.UseOnUrl("/timeTaken", async context =>
            {
                await Task.Delay(99999999);
                await context.Response.WriteAsync($"Time now is {DateTime.Now-dateTime}");
            });

            app.UseOnUrl("/stats", async context =>
            {
                foreach (var x in keyValues)
                {
                    if (x.Key.Contains("stats"))
                    {
                        continue;
                    }
                    await context.Response.WriteAsync($"{context.Request.Scheme}://{context.Request.Host}{x.Key} : {x.Value}<br/>");
                }
            });

            app.UseOnUrl("/unhandledUrls", async context =>
            {
                foreach (var x in keyValuePairs)
                {
                    await context.Response.WriteAsync($"{context.Request.Scheme}://{context.Request.Host}{x.Key} : {x.Value}<br/>");
                }
            });

            app.Use(next => async context =>
            {
                await CaptureUrl(context.Request.Path.Value, keyValuePairs);
                await next(context);
            });

            async Task CaptureUrl(string url, Dictionary<string, int> keyValues)
            {
                if (keyValues.ContainsKey(url))
                {
                    keyValues[url] += 1;
                }
                else
                {
                    keyValues.Add(url, 1);
                }
            }
        }
    }
}
