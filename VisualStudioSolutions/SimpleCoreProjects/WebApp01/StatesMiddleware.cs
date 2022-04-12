using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01
{
    public static class StatesMiddleware
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
