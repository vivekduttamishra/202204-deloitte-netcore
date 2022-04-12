using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Middlewares
{
    public static class CustomExceptionHandlesMiddleWare
    {
        public static void CustomDevExceptionHandler(this IApplicationBuilder app)
        {
            app.Use(next => async context =>
            {
                try
                {
                    await next(context);
                }
                catch (Exception e)
                {
                    await context.Response.WriteAsync("In developer Exception Page\n");
                    await context.Response.WriteAsync($"Stack Trace :: {e.StackTrace} \n Exception Message : {e.Message}");
                }
            });
        }
        public static void CustomProdExceptionHandler(this IApplicationBuilder app)
        {
            app.Use(next => async context =>
            {
                try
                {
                    await next(context);
                }
                catch (Exception e)
                {
                    await context.Response.WriteAsync("Error . facing error while accesing the page \n");
                    await context.Response.WriteAsync("Please try again after some time\n");
                    await context.Response.WriteAsync("Please contact developement team if the still exists ");

                }
            });
        }
    }
}


