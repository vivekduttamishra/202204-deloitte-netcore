using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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


        public static void UseBefore(this IApplicationBuilder app, RequestDelegate action)
        {
            app.Use(next => async context =>
           {
               await action(context);
               await next(context);
           });
        }


        public static void UseAfter(this IApplicationBuilder app, RequestDelegate action)
        {
            app.Use(next => async context =>
            {   
                await next(context);
                await action(context);
            });
        }


       public static void UsePeformanceLogger(this IApplicationBuilder app)
        {
            app.Use( next => async context =>
            {
                var logger = context.RequestServices.GetService<ILogger<object>>();
                var stopWatch= Stopwatch.StartNew();
                await next(context);
                stopWatch.Stop();
                logger.LogInformation($"{context.Request.Path} took {stopWatch.ElapsedMilliseconds} ms to complete");              
                

            });
        }


    }
}
