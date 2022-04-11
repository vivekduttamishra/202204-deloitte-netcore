using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace coreapp
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
                if(context.Request.Path.Value.Contains("/log"))
                {
                    await context.Response.WriteAsync($"<h1>Long running task started</h1><hr/>");
                    System.Diagnostics.Stopwatch _timer = new System.Diagnostics.Stopwatch();
                    _timer.Start();
                    Console.WriteLine("Long running task");
                    for(int i = 0; i < Convert.ToInt32(context.Request.Query["Range"].ToString()); i++){
                        Console.WriteLine(i);
                    }
                    _timer.Stop();
                    await context.Response.WriteAsync($"Time taken to exeute request is {_timer.ElapsedMilliseconds} milliseconds");

                }else
                {
                    await next(context);
                }
            });

              app.Use(next => {
               RequestDelegate thisMiddleware = async context => {
                   await context.Response.WriteAsync($"Unknown request - {context.Request.Path}");
               };
               return thisMiddleware;
           });
        }
    }
}
