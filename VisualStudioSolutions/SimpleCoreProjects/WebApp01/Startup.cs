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

            app.Use(next => async context =>
           {

               await next(context);
               await context.Response.WriteAsync("<hr/><p>This is footer</p>");

           });

            app.Use(next => async context =>
            {

                await context.Response.WriteAsync($"<h1>Common Meessage</h1><hr/>");
                await next(context);
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

            Middlewares.UseOnUrl(app, "/time", async context =>
             {
                 await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

             });


        }
    }
}
