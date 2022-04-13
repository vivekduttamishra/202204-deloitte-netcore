using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp01.Services;

namespace WebApp01.MiddleWares
{
    public static class StatsMiddlewares
    {
        public static void UseStats(this IApplicationBuilder app)
        {
            IUrlService service = app.ApplicationServices.GetService<IUrlService>();

            app.Use(next => async context =>
            {
                await service.StoreUrls(context.Request.Path);
                await next(context);
            });

            app.UseOnUrl("/stats", async context =>
            {
                var service = context.RequestServices.GetService<IUrlService>();

                Dictionary<string, int> keyValues = await service.GetStats();

                await PrintResponse(context, keyValues);
            });

            app.UseOnUrl("/404", async context =>
            {
                var service = context.RequestServices.GetService<IUrlService>();

                Dictionary<string, int> keyValues = await service.GetUnHandledUrls();

                await PrintResponse(context, keyValues);
            });            
        }
        private static async Task PrintResponse(HttpContext context, Dictionary<string, int> keyValuePairs)
        {
            foreach (var x in keyValuePairs)
            {
                if (x.Key.Contains("404") || x.Key.Contains("stats"))
                {
                    continue;
                }
                await context.Response.WriteAsync($"{context.Request.Scheme}://{context.Request.Host}{x.Key} : {x.Value}<br/>");
            }
        }
    }
}
