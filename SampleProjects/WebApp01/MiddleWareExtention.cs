using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApp01
{
    public static class MiddleWareExtention
    {
        public static void UseOnUrl(this IApplicationBuilder app, string url, RequestDelegate action)
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
