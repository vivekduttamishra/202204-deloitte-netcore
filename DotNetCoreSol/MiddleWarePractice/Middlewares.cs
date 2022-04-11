using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace MiddleWarePractice
{
    public static class Middlewares
    {
        public static void UseOnUrl(this IApplicationBuilder app, string url, RequestDelegate requestDelegate)
        {
            app.Use(next => async context =>
            {
                if(context.Request.Path.Value == url)
                {
                    await requestDelegate(context);
                }
                else
                {
                    await next(context);
                }
            });
        }

        public static void UseStats(this IApplicationBuilder app, Action<string, ErrorType> action)
        {
            app.Use(next => async context =>
            {
                await next(context);
                if(context.Response.StatusCode == 200)
                action($"{context.Request.Path.Value}{context.Request.QueryString.Value}", ErrorType.Success);
                else
                action($"{context.Request.Path.Value}{context.Request.QueryString.Value}", ErrorType.Failed);

            });
        }
    }
}
