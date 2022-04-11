using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            app.Use(next => async context =>
            {
                await next(context);
                await context.Response.WriteAsync($"<h1/><p>www.google.com</p>");

            });
            app.Use(next => async context =>
            {
                await context.Response.WriteAsync($"<h1>Google</h1><hr/>");
                await next(context);
            });

            app.Use(next => async context =>
            {
                if(context.Request.Path.Value.Contains("/longrunning"))
                {
                    await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

                }else
                {
                    await next(context); 
                }
            });
            app.Use(next => async context =>
            {
                if (context.Request.Path.Value.Contains("/timetaken"))
                {
                    await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

                }
                else
                {
                    await next(context);
                }
            });
            app.Use (  next => async context =>
            {
                Console.WriteLine($"Received {context.Request.Method} {context.Request.Path} " );                
                await next(context); 
            });
            app.Use(next => async context =>
            {
                Console.WriteLine($"Received {context.Request.Method} {"/testing"} ");
                await next(context);
            });
            app.Use(next => async context =>
            {
                Console.WriteLine($"Received {context.Request.Method} {"/testing2"} ");
                await next(context);
            });
            app.Use(next => {

                RequestDelegate thisMiddleware = async context =>
                {                    
                    await context.Response.WriteAsync($"Handled {context.Request.Path}");
                };

                return thisMiddleware;
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync($"Time: {DateTime.Now.ToLongTimeString()}");
            });


            //unreachable code
            app.Run(new RequestDelegate(Greet));

        }

        private async Task Greet(HttpContext context)
        {
            await context.Response.WriteAsync($"Welcome to .net core {context.Request.Path} ");
        }
    }
}
