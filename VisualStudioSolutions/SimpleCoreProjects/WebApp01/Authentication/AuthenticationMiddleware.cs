using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApp01.Middlewares;

namespace WebApp01.Authentication
{
    public static class AuthenticationMiddleware
    {
        public static void UseUserAuthentication(this IApplicationBuilder app)
        {
            //configure all the end points that we may need for implementing authentication

            var service = app.ApplicationServices.GetService<IUserService>();

            app.UseBefore( async context =>
            {
                var token = context.Request.Headers["Authorization"];
                if(!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        var user = await service.ValidateToken(token);
                        //I want this 'user' object to be available to other middlewares
                        context.Request.Headers["UserEmail"] = user.Email;
                        context.Request.Headers["UserName"] = user.Name;

                    }catch(InvalidCredentialsException ex)
                    {
                        //It is ok if there is no information
                    }
                }


            });

            app.UseOnUrl("/register", async context =>
            {
                var qs = context.Request.Query;
                var name = qs["name"];
                var email = qs["email"];
                var password = qs["password"];

                var user = new User() { Name = name, Email = email, Password=password };
                try
                {
                    await service.AddUser(user);
                    var token = await service.GetToken(email, password);
                    await context.Response.WriteAsync(token);
                } 
                catch(DuplicateUserException ex)
                {
                    context.Response.StatusCode = 400; //Bad Request
                    await context.Response.WriteAsync(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    context.Response.StatusCode = 400; //Bad Request
                    await context.Response.WriteAsync(ex.Message);
                }
                catch (InvalidCredentialsException ex)
                {
                    context.Response.StatusCode = 401; //Bad Request
                    await context.Response.WriteAsync(ex.Message);
                }
            });

            app.UseOnUrl("/login", async context =>
            {
                var qs = context.Request.Query;
                
                var email = qs["email"];
                var password = qs["password"];

                try
                {
                    var token = await service.GetToken(email, password);
                    await context.Response.WriteAsync(token);
                }
                catch (ArgumentException ex)
                {
                    context.Response.StatusCode = 400; //Bad Request
                    await context.Response.WriteAsync(ex.Message);
                }
                catch (InvalidCredentialsException ex)
                {
                    context.Response.StatusCode = 401; //Bad Request
                    await context.Response.WriteAsync(ex.Message);
                }
            });

            app.UseOnUrl("/logout", async context =>
            {
                var token = context.Request.Headers["Authorization"];
                if(!string.IsNullOrEmpty(token))
                {
                    await service.DeleteToken(token);
                    context.Request.Headers["UserName"] = "";
                    context.Request.Headers["UserEmail"] = "";
                }
            });

        }

        public static void UseProtectedRoute(this IApplicationBuilder app,string url, RequestDelegate action, Action<UseOnUrlOptions> config=null)
        {
            app.UseOnUrl( url, async context =>
            {
                //check if user is logged in
                var userName = context.Request.Headers["UserName"];
                if(string.IsNullOrEmpty(userName))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Missing/Invalid token");
                } else
                {
                    await action(context);
                }

            }, config);
        }
    
        public static void UseProtectAll(this IApplicationBuilder app)
        {
            app.Use(next => async context =>
              {
                  var user = context.Request.Headers["UserName"];
                  if (string.IsNullOrEmpty(user))
                  {
                      context.Response.StatusCode = 401;
                      await context.Response.WriteAsync("invalid/missing token");

                  }
                  else
                  {
                      await next(context);
                  }
              });
        }
    
    }
}





