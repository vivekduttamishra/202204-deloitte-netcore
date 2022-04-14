using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp01.Service;

namespace WebApp01
{
    public static class Middlewares
    {

        public static void UseOnUrl(this IApplicationBuilder app, string url, RequestDelegate action)
        {
            IDisplayTextService service = app.ApplicationServices.GetService<IDisplayTextService>();
            app.Use(next => async context =>
             {
                 if (context.Request.Path.Value == url)
                 {
                     await action(context);
                 }
                 else
                 {
                     await next(context);
                     if (context.Response.StatusCode == 404 && context.Request.Path.Value != "/")
                     {

                         service.Add404Urls(context.Request.GetDisplayUrl());

                     }
                 }
             });
        }
    }
}
