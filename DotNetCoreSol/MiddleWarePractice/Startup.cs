using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiddleWarePractice.Interfaces;
using MiddleWarePractice.Services;
using System;

namespace MiddleWarePractice
{
    public class Startup
    {
        

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStatsService, StatsService>();
        }

       


        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStatsService statsService)
        {
            if (env.IsDevelopment())
                app.myDevExceptionHandle();
            else
                app.myProdExceptionHandle();   

            app.UseStats();

            app.UseOnUrl("/books", async context =>
            {
                throw new Exception("my own exceptions");
                await context.Response.WriteAsync($"Book Details");
            });

            app.UseOnUrl("/authors", async context =>
            {
                await context.Response.WriteAsync($"author Details");
            });

            app.UseOnUrl("/stats", async context =>
            {
                await context.Response.WriteAsync("<h1>Valid URL's Visited</h1> <br>");
                await context.Response.WriteAsync(statsService.GetValidUrlStats());

            });

            app.UseOnUrl("/404", async context =>
            {
               await context.Response.WriteAsync("<h1>Invalid URL's Visited</h1> <br>");
               await context.Response.WriteAsync(statsService.GetInvalidUrlStats());
            });

            app.UseOnUrl("/error", async context =>
            {
                await context.Response.WriteAsync($"Error Occured");
            });
        }
    }
}