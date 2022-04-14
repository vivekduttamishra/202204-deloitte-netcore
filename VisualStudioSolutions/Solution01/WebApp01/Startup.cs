using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using WebApp01.Service;

namespace WebApp01
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDisplayTextService, ShowStatsService>();
            services.AddSingleton<IDisplayTokenService, AuthenticationService>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDisplayTextService displayTextService, IDisplayTokenService displayTokenService)
        {
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //app.Use(next => async context =>
            //{
            //    await next(context);
            //    await context.Response.WriteAsync("<hr/><p>Thank You!</p>");

            //});

            //app.UseOnUrl("/long-running", async context =>
            //{
            //    await context.Response.WriteAsync("Long running The work started...");
            //    await Task.Delay(2000);
            //    stopwatch.Stop();
            //    await context.Response.WriteAsync("Long running workd finished...");
            //    await context.Response.WriteAsync($"Total time taken{stopwatch.ElapsedMilliseconds}");

            //});
            app.UseOnUrl("/time", async context =>
            {
                displayTextService.AddStats(context.Request.GetDisplayUrl());
                await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");
            });

            app.UseOnUrl("/Date", async context =>
            {
                displayTextService.AddStats(context.Request.GetDisplayUrl());
                await context.Response.WriteAsync($"Date now is {DateTime.Now.ToLongDateString()}");
            });

            app.UseOnUrl("/stats", async context =>
            {
                var message = displayTextService.DisplayText();
                await context.Response.WriteAsync($"{message}");
            });

            app.UseOnUrl("/404", async context =>
            {
                var message = displayTextService.ShowWrongUrlList();
                await context.Response.WriteAsync($"{message}");
            });

            app.UseOnUrl("/Register", async context =>
            {
                var message = displayTokenService.RegisterUser(context.Request.Query["name"], context.Request.Query["email"], context.Request.Query["password"]);
                await context.Response.WriteAsync($"{message}");
            });
            app.UseOnUrl("/Login", async context =>
            {
                var message = displayTokenService.LoginUser(context.Request.Query["email"], context.Request.Query["password"]);
                await context.Response.WriteAsync($"{message}");
            });
            app.UseOnUrl("/Protected", async context =>
            {
                var url = displayTokenService.AddTokenToUrl(context.Request.GetDisplayUrl());
                var message = displayTokenService.CheckIfAuthorized(url);
                await context.Response.WriteAsync($"{message}");
            });
            app.UseOnUrl("/LogOut", async context =>
            {
                var message = displayTokenService.LogOut();
                await context.Response.WriteAsync($"{message}");
            });
            app.UseRouting();
            app.UseEndpoints(config =>
            {
                config.MapControllerRoute("DefaultRoute",
                        "{controller=Home}/{action=Welcome}/{id?}"
                    );
            });

        }
    }
}
