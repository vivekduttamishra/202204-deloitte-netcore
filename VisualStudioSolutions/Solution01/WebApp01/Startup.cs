using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApp01
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //app.Use(next => async context =>
            //{
            //    await next(context);
            //    await context.Response.WriteAsync("<hr/><p>Thank You!</p>");

            //});

            //app.UseOnUrl("/long-running", async context =>
            //{
            //    await context.Response.WriteAsync("Long running The work started...");
            //    await Task.Delay(2000);
            //    stopwatch.Stop();
            //    await context.Response.WriteAsync("Long running workd finished...");
            //    await context.Response.WriteAsync($"Total time taken{stopwatch.ElapsedMilliseconds}");

            //});

            app.UseOnUrl("/time", async context =>
            {
                await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");
            });

            app.UseOnUrl("/Date", async context =>
            {
                await context.Response.WriteAsync($"Date now is {DateTime.Now.ToLongDateString()}");
            });

            app.UseOnUrl("/stats", async context =>
            {
                await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");
            });

            app.UseOnUrl("/404", async context =>
            {
                await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");
            });
        }
    }
}
