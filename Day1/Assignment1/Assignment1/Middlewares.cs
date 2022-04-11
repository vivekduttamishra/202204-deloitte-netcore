using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Assignment1
{
    public static class Middlewares
    {
        public static void UseOnUrl(this IApplicationBuilder app, string url, RequestDelegate action)
        {
            
            app.Use(next => async context =>
            {
                
                if (context.Request.Path.Value.Equals(url, StringComparison.OrdinalIgnoreCase))
                {
                    ValidRouteFrequency.Add(context.Request.Path.Value + context.Request.QueryString.Value);
                    await action(context);
                    
                }                    
                else
                {
                    InvalidRouteFrequency.Add(context.Request.Path.Value + context.Request.QueryString.Value);
                    await next(context);
                    
                }                    
            });
        }
    }

    
}
