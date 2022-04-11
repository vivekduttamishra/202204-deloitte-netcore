using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            int bookcount = 0;
            int timecount = 0;
            int count = 0;
            app.UseOnUrl("/books", async context =>
            {
                bookcount++;
                await context.Response.WriteAsync($"{context.Request.Host}/{context.Request.Path}: {bookcount}");
            });

            app.UseOnUrl("/time", async context =>
            {
                timecount++;
                await context.Response.WriteAsync($"{context.Request.Host}/{context.Request.Path}: {timecount}");
            });

            app.UseOnUrl("/404", async context =>
            {
                count++;
                await context.Response.WriteAsync($"<p>{context.Request.Host}/{context.Request.Path}: {count}</p>");
            });

            app.UseOnUrl("/stats", async context =>
            {
                await context.Response.WriteAsync($"<p>{context.Request.Host}/books: {bookcount}</p>");
                await context.Response.WriteAsync($"<p>{context.Request.Host}/time: {timecount}</p>");
                await context.Response.WriteAsync($"<p>{context.Request.Host}/404: {count}</p>");
            });

            app.Use(next => {

                RequestDelegate thisMiddleware = async context =>
                {
                    //perform whatever you want to
                    await context.Response.WriteAsync($"{context.Request.Host}/{context.Request.Path}");
                };

                return thisMiddleware;
            });

            //unreachable code
            app.Run(new RequestDelegate(Greet));
        }

        private async Task Greet(HttpContext context)
        {
            await context.Response.WriteAsync($"Hello World to {context.Request.Path} ");
        }
    }
}
