using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp01.Services;

namespace WebApp01.MyMiddlewares
{
    public static class StatusMiddlewares
    {

        public static void UseStats(this IApplicationBuilder app)
        {

            IUrlStatsService service = app.ApplicationServices.GetService<IUrlStatsService>();
            app.UseStaticFiles();

            app.Use( next => async context =>
            {
                await service.AddVisitedUrl(context.Request.Path);

                await next(context);
            });


            app.UseOnUrl( "/stats", async context =>
            {
                var html = new StringBuilder("<html><head><title>Urls Stats</title></head><body><h1>Urls Stats</h1>");

                html.Append("<table><tr><th>Url</th><th>Visit Count</th></tr>");

                var stats = await service.GetStats();
                foreach(var url in stats.Keys)
                {
                    html.Append($"<tr><td>{url}</td><td>{stats[url]}</td></tr>");
                }

                html.Append("</table></body></html>");

                await context.Response.WriteAsync(html.ToString());
            });
            
        }


    }
}
