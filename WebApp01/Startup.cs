using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            app.Use(next => async context =>
            {
                //display a common message
                await context.Response.WriteAsync($"<h1>Book's Web</h1><hr/>");
                //let other middleware do whatever they want.
                await next(context);  //pass control to the next middleware
            });

            var time1 = DateTime.Now;

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

            app.UseOnUrl("/stats", async context =>
            {
              foreach(var url in Middlewares.acceptedUrlStats)
                {
                    await context.Response.WriteAsync($"https://localhost:44306{url.Key} : {url.Value}\r\n");
                }
            });
            app.UseOnUrl("/404", async context =>
            {
                foreach (var url in Middlewares.otherUrlStats)
                {
                    await context.Response.WriteAsync($"https://localhost:44306{url.Key} : {url.Value}\r\n");
                }
            });

            app.Use(next => async context =>
            {
               if(Middlewares.acceptedUrl==true)
                {
                    await next(context);
                    await context.Response.WriteAsync($"<hr/>This is the footer<hr/>");
                }
                else
                {
                    Middlewares.UpdateOtherUrlStats(context.Request.Path.Value);
                }
                
              
            });


            app.Use(next => async context => {

                await context.Response.WriteAsync($"<hr/>This task took {(DateTime.Now - time1).Milliseconds} ms to complete.");
               
            });



            #region old code
            //var time1 = DateTime.Now;

            //app.Use(next => async context =>
            //{
            //    if ((context.Request.Path.Value.Contains("/assignment")) ||(context.Request.Path.Value.Contains("/Assignment")))
            //    {
            //        await context.Response.WriteAsync($"<h1>This is Assignment 1.1.a</h1>");
            //    }
            //    else
            //    {
            //        await context.Response.WriteAsync($"<h1>Other Requests {context.Request.Path}</h1>");
            //    }
            //    await next(context);
            //});

            //app.Use(next => async context => {

            //    await next(context);
            //    await context.Response.WriteAsync($"<hr/>This is the footer<hr/>");
            //});


            //app.Use(next => async context => {

            //    await context.Response.WriteAsync($"This task took {(DateTime.Now - time1).Milliseconds} ms to complete.");

            //});
            #endregion


        }
    }
}
