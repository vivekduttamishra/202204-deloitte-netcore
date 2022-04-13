using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WebApp01.MiddleWares;
using WebApp01.Services;

namespace WebApp01
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUrlService, StatsService>();
            services.AddSingleton<IUserAthenticationService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.AuthenticateUser();

            app.UseStats();

            app.UseOnUrl("/User", async context =>
            {
                if(!context.Request.Query.ContainsKey("token"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync($"{context.Response.StatusCode}");
                }
                else
                {
                    User user = new User()
                    {
                        Email = context.Request.Query["email"],
                        Token = context.Request.Query["token"]
                    };
                    IUserAthenticationService service = app.ApplicationServices.GetService<IUserAthenticationService>();
                    bool IsValidToken = await service.IsValidToken(user);
                    if (IsValidToken)
                    {
                        User user1 = await service.GetUser(user.Email);
                        await context.Response.WriteAsync($"User Name is {user1.Name}");
                    }
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync($"{context.Response.StatusCode}");
                }
            });

            app.UseOnUrl("/Hello", async context =>
            {
                await context.Response.WriteAsync($"{context.Request.Path.Value}");
            });

            app.UseOnUrl("/long-running", async context =>
            {
                await context.Response.WriteAsync("Long running The work started...</br>");
                await Task.Delay(2000);
                await context.Response.WriteAsync("Long running workd finished...");
            });

            app.UseOnUrl("/date", async context =>
            {
                await context.Response.WriteAsync($"Date is : {DateTime.Now.ToLongDateString()}");
            });

            app.UseOnUrl("/time", async context =>
            {
                await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");
            });

            app.UseOnUrl("/totalTimeTaken", async context =>
            {
                await Task.Delay(2000);
            });

            app.Use(next => async context =>
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                IUrlService service = app.ApplicationServices.GetService<IUrlService>();
                await service.StoreUnHandledUrls(context.Request.Path.Value);
            });
        }
    }
}
