using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MiddleWarePractice.Interfaces;
using System;

namespace MiddleWarePractice
{
    public static class Middlewares
    {
        public static void UseOnUrl(this IApplicationBuilder app, string url, RequestDelegate requestDelegate)
        {
            app.Use(next => async context =>
            {
                if (context.Request.Path.Value == url)
                {
                    await requestDelegate(context);
                }
                else
                {
                    await next(context);
                }
            });
        }

        public static void UseProctectedRoute(this IApplicationBuilder app, string url, RequestDelegate requestDelegate)
        {
            app.Use(next => async context =>
            {
               var userService = context.RequestServices.GetService<IUserManagementService>();
                var token = context.Request.Query["token"];

                if(context.Request.Path.Value != url)
                {
                    await next(context);
                }
                else if (context.Request.Path.Value == url && userService.ValidateToken(token))
                {
                    await requestDelegate(context);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("unauthroized");
                }
            });
        }

        public static void UseStats(this IApplicationBuilder app)
        {
            var statService = app.ApplicationServices.GetService<IStatsService>();
            app.Use(next => async context =>
            {
                await next(context);
                if (context.Response.StatusCode == 200)
                    statService.AddValidUrlCount($"{context.Request.Path.Value}{context.Request.QueryString.Value}");
                else
                    statService.AddInvalidUrlCount($"{context.Request.Path.Value}{context.Request.QueryString.Value}");
            });
        }
        public static void myDevExceptionHandle(this IApplicationBuilder app)
        {
            app.Use(next => async context =>
            {
                try
                {
                    await next(context);

                }
                catch (Exception ex)
                {
                    await context.Response.WriteAsync($"{ex.Message} \n {ex.StackTrace}");
                }
            });
        }

        public static void myProdExceptionHandle(this IApplicationBuilder app)
        {
            app.Use(next => async context =>
            {
                try
                {
                    await next(context);

                }
                catch (Exception)
                {
                    context.Response.Redirect("/error");
                }
            });
        }

    }
}
