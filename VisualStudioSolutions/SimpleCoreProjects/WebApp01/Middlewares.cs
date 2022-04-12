using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01
{
    public static class Middlewares
    {
        public static void UseOnUrl(this IApplicationBuilder app, string url, RequestDelegate action)
        {
            app.Use(next => async context =>
            {
                if (context.Request.Path.Value == url)
                    await action(context);
                else
                    await next(context);
            });
        }
        public static void ErrorandStatConfiguration(this IApplicationBuilder app, Action<string, string> action)
        {
            app.Use(next => async context =>
            {
                await next(context);
                if (context.Response.StatusCode == 404)
                    action($"{context.Request.Path.Value}{context.Request.QueryString.Value}", "NotFound");
                else
                    action($"{context.Request.Path.Value}{context.Request.QueryString.Value}", "Found");

            });
        }
    }
}

