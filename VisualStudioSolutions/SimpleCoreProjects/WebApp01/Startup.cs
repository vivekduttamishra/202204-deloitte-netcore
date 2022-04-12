using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace WebApp01
{
    public class Startup
    {
        public int BooksCount;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.StatsUrl("/books", async context =>
            {
                BooksCount += 1;
                await context.Response.WriteAsync("Books");
            });

            app.StatsUrl("/stats", async context =>
            {
                await context.Response.WriteAsync($"https://localhost:5000/books : {BooksCount}");
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
