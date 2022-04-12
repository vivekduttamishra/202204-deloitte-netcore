using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp01.Services;

namespace WebApp01.Middlewares
{
    public static class UserManagementMiddleWare
    {
        public static void UserManagementService(this IApplicationBuilder app)
        {
            ISimpleUserManagementService service = app.ApplicationServices.GetService<ISimpleUserManagementService>();

            app.UseOnUrl("/Register", async context =>
            {
                var username = context.Request.Query["username"].ToString();
                var password = context.Request.Query["password"].ToString();
                var email = context.Request.Query["email"].ToString();
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)&& !string.IsNullOrEmpty(email))
                {
                    var registeredToken = service.Register(username, email, password);
                    if (registeredToken == null)
                    {
                        await context.Response.WriteAsync("User already registered");
                    }
                    else
                    {
                        await context.Response.WriteAsync("User registered sucessfully\n");
                        await context.Response.WriteAsync($"Token is : {registeredToken}");
                    }
                }
                else
                {
                    await context.Response.WriteAsync("User provided details cannot be empty, please provide all the fields");
                }
            });
            app.UseOnUrl("/login", async context =>
            {
                var username = context.Request.Query["username"].ToString();
                var password = context.Request.Query["password"].ToString();

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    var usertoken = service.Login(username, password);
                    if ( usertoken != null )
                    {
                        await context.Response.WriteAsync($"User Logged in Successfully and token is: {usertoken}");
                    }
                    else
                    {
                        await context.Response.WriteAsync($"Please enter correct username/password");
                    }

                }
                else
                {
                    await context.Response.WriteAsync($"please provide username and password");
                }
            });
            app.UseOnUrl("/logout", async context =>
            {
                var username = context.Request.Query["username"].ToString();

                if (!string.IsNullOrEmpty(username))
                {
                    service.Logout(username);
                    await context.Response.WriteAsync($"User Logged out Successfully ");
                }
                else
                {
                    await context.Response.WriteAsync($"please provide username");
                }
            });
            

        }
    }
}
