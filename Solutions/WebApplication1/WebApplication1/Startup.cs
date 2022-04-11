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

namespace WebApplication1
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            Stopwatch stopwatch = new Stopwatch();
            app.Use(next => async context =>
            {
                stopwatch.Restart();
                if (context.Request.Path.Value.Contains("/clock"))
                {
                    await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

                    await next(context);
                }
                else
                {
                    await next(context); //I don't handle this url. let someone else handle it. I don't care.
                }
            });
            app.Use(next => async context =>
            {
                await Task.Delay(2000);
                await context.Response.WriteAsync("<p> this is footer </p>");
                await next(context);
            });

            app.Use(next => async context =>
            {
                stopwatch.Stop();
                Console.WriteLine($"time take for request to serve is {stopwatch.ElapsedMilliseconds} milliseconds");
            });
            app.UseRouting();


          
        }
    }
}
