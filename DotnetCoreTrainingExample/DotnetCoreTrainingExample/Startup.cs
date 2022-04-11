using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreTrainingExample
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
                if (context.Request.Path.Value.Contains("/time"))
                {
                    await context.Response.WriteAsync($"Doing Assignment at Time : {DateTime.Now.ToLongTimeString()}");

                }
                else
                {
                    await next(context); //I don't handle this url. let someone else handle it. I don't care.
                }
            });

            app.Use(next => async context =>
            {
                //display a common message
                await context.Response.WriteAsync($"<h1>Sample Page</h1><hr/>");
                //let other middleware do whatever they want.
                await next(context);  //pass control to the next middleware
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
