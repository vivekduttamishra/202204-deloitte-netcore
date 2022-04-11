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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            

            app.Use(next => async context =>            
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                await next(context);
                await context.Response.WriteAsync($"<p> this is footter </p>");
                stopwatch.Stop();
                Console.WriteLine($"time take for request to serve is {stopwatch.ElapsedMilliseconds} milliseconds");
            });

            app.Use(next => async context =>
            {
                if (context.Request.Path.Value.Contains("/books"))
                {
                await Task.Delay(2000);
                    await context.Response.WriteAsync("book details");
                }
                else
                {
                    await next(context);
                }
            });

            app.Use(next => async context =>
            {
                if (context.Request.Path.Value.Contains("/authors"))
                {
                    await Task.Delay(3000);
                    await context.Response.WriteAsync("author details");
                }
                else
                {
                    await next(context);
                }
            });
        }
    }
}
