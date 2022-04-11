using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

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
            DateTime dateTime = DateTime.Now;

            app.Use(next => async context =>
            {
              
                await context.Response.WriteAsync($"<h1>Book's Web</h1><hr/>");
                await next(context); 
            });

            app.Use(next => async context =>
            {
                if (context.Request.Path.Value.Contains("1"))
                {
                    await context.Response.WriteAsync($"{context.Request.Path}");
                    await next(context);
                }
                else
                {
                    await next(context);
                }
            });
            
            app.Use(next => async context =>
            {
                await context.Response.WriteAsync($"{"Long running task started"}");
                await Task.Delay(2000);
                await context.Response.WriteAsync($"{"Long running task finished"}");
                await next(context);
            });

            app.Use(next => async context =>
            {
                await context.Response.WriteAsync($"{DateTime.Now - dateTime}");
                await next(context);
            });

            app.Use(next => async context =>
            {
                await context.Response.WriteAsync($"<hr/><h1>@Deloitte</h1>");
            });
        }
    }
}
