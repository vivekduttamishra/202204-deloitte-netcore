using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01
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


    public static class Middlewares
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
    }
}
