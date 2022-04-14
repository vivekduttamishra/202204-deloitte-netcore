using Microsoft.AspNetCore.Builder;
using WebApp01.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace WebApp01.MiddleWares
{
    public static class UserAuthenticationMiddleware
    {
        public static void AuthenticateUser(this IApplicationBuilder app)
        {
            IUserAthenticationService service = app.ApplicationServices.GetService<IUserAthenticationService>();

            app.UseOnUrl("/register", async context =>
            {
                User user = new User()
                {
                    Name = context.Request.Query["name"],
                    Email = context.Request.Query["email"],
                    Password = context.Request.Query["password"]
                };
                var service = context.RequestServices.GetService<IUserAthenticationService>();
                string userToken = await service.Register(user);
                await context.Response.WriteAsync($"{userToken}");
            });

            app.UseOnUrl("/login", async context =>
            {
                User user = new User()
                {
                    Email = context.Request.Query["email"],
                    Password = context.Request.Query["password"]
                };
                var service = context.RequestServices.GetService<IUserAthenticationService>();
                string userToken = await service.Login(user);
                await context.Response.WriteAsync($"{userToken}");
            });

            app.UseOnUrl("/logout", async context =>
            {
                User user = new User()
                {
                    Email = context.Request.Query["email"]
                };
                var service = context.RequestServices.GetService<IUserAthenticationService>();
                string userToken = await service.Logout(user);
                await context.Response.WriteAsync($"{userToken}");
            });
        }
    }
}
