using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp01.Services;

using Microsoft.Extensions.DependencyInjection;
namespace WebApp01.Middlewares
{

    public enum MatchType
    {
        Exact,
        StartsWith,
        Contains
    }

    public class UseOnUrlOptions
    {
        public MatchType MatchType { get; set; } = MatchType.Exact;
        public bool MatchCase { get; set; } = false;
    }


    public static class MyMiddlewares
    {
        public static void UseOnUrl(this IApplicationBuilder app, string url, RequestDelegate action, Action<UseOnUrlOptions> configure=null)
        {
            var opt = new UseOnUrlOptions(); //get the default option
            //it can be overriden by the user            
            configure?.Invoke(opt);

            app.Use( next => async context =>
            {
                var requestPath = context.Request.Path.Value;

                if(!opt.MatchCase)
                {
                    requestPath = requestPath.ToLower();
                    url = url.ToLower();
                }

                bool match = false;
                if (opt.MatchType == MatchType.Exact)
                    match = url == requestPath;
                else if (opt.MatchType == MatchType.StartsWith)
                    match = requestPath.StartsWith(url);
                else
                    match = requestPath.Contains(url);



                if (match)
                    await action(context);
                else
                    await next(context);
            });
        }

        public static void UseProctectedRoute(this IApplicationBuilder app, string url, RequestDelegate requestDelegate)
        {
            app.Use(next => async context =>
            {
                var service = context.RequestServices.GetService<ISimpleUserManagementService>();
                var token = context.Request.Query["token"];

                if (context.Request.Path.Value != url)
       
                {
                    await next(context);
                }
                else if (context.Request.Path.Value == url && service.TokenAuthentication(token))
                {
                    await requestDelegate(context);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token is not process, so unauthroized");
                }
            });
        }
    }
}
