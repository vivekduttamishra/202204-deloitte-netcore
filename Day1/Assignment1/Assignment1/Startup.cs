using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1
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
        //https://localhost:5000/books 
        //https://localhost:5000/books?id=2 
        //https://localhost:5000/books?id=4 
        //https://localhost:5000/books 
        //https://localhost:5000/time 
        //https://localhost:5000/books?id=2 
        //https://localhost:5000/books 
        //https://localhost:5000/books?id=2 

            app.Use(next => async context =>
            {
                //display a common message
                await context.Response.WriteAsync($"<h1>Book's Web</h1><hr/>");
                //let other middleware do whatever they want.
                await next(context);  //pass control to the next middleware
            });

            app.UseOnUrl("/books", async context =>
            {
                var id = context.Request.Query["id"].ToString();

                if(string.IsNullOrEmpty(id))
                {
                    await context.Response.WriteAsync("All Books...");
                }
                else
                {
                    await context.Response.WriteAsync($"Book - {id}");
                }
            });

            app.UseOnUrl("/time", async context =>
            {
                await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

            });

            app.UseOnUrl("/stats", async context =>
            {
                await context.Response.WriteAsync("All Valid Routes...<hr/>");
                foreach (var route in ValidRouteFrequency.GetLog())
                {
                    await context.Response.WriteAsync($"{route.Key} : <b>{route.Value}</b> <br/>");
                }
            });

            app.UseOnUrl("/404", async context =>
            {
                await context.Response.WriteAsync("All Invalid Routes...<hr/>");
                foreach (var route in InvalidRouteFrequency.GetLog())
                {
                    await context.Response.WriteAsync($"{route.Key} : <b>{route.Value}</b> <br/>");
                }
            });
        }
    }
}
