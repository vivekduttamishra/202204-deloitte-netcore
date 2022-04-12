using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public static class StatsMiddleware
    {
        public static void StatsUrl(this IApplicationBuilder app, string url, RequestDelegate action)
        {
            app.Use(next => async context =>
            {
                if (context.Request.Path.Value == url)
                    await action(context);
                else
                    await next(context);
            });
        }
    }
}