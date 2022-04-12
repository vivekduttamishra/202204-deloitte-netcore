using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp01
{
    public class Startup
    {
        private Dictionary<string, int> StatUrlsCount = new Dictionary<string, int>();
        private Dictionary<string, int> NotFoundUrls = new Dictionary<string, int>();
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.Use( next => async context =>
            {

                await next(context); //let the other middleware work
                //now I will add the footer
                await context.Response.WriteAsync("<hr/><p>&copy; http://conceptarchitect.in</p>");

            });

            app.Use(next => async context =>
            {
                //display a common message
                await context.Response.WriteAsync($"<h1>Book's Web</h1><hr/>");
                //let other middleware do whatever they want.
                await next(context);  //pass control to the next middleware
            });


            app.UseOnUrl("/long-running",  async context =>
            {
                
                await context.Response.WriteAsync("Long running The work started...");
                await Task.Delay(2000);
                await context.Response.WriteAsync("Long running workd finished...");

            });

            app.UseOnUrl("/date", async context =>
            {
                await context.Response.WriteAsync($"Date is : {DateTime.Now.ToLongDateString()}");
            });

            Middlewares.UseOnUrl(app,"/time", async context =>
            {
                  await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

            });
            app.ErrorandStatConfiguration((url, err) => AddUrlsToDictionary(url, err));

            app.UseOnUrl("/stats", async context =>
            {
                await context.Response.WriteAsync("<h1>URL's </h1> <br>");
                await PrintUrls(context, StatUrlsCount);
            });

            app.UseOnUrl("/404", async context =>
            {
                await context.Response.WriteAsync("<h1>Unhandled Urls</h1> <br>");
                await PrintUrls(context, NotFoundUrls);
            });


            app.Use (next => async context =>
            {
                //this middleware just logs the information
                Console.WriteLine($"Received {context.Request.Method} {context.Request.Path} " );
                //there is not visible output here.
                await next(context);  //pass control to the next middleware
            });


            app.Use(next => {

                RequestDelegate thisMiddleware = async context =>
                {
                    //perform whatever you want to
                    await context.Response.WriteAsync($"Handled {context.Request.Path}");
                };

                return thisMiddleware;
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync($"Time: {DateTime.Now.ToLongTimeString()}");
            });


            //unreachable code
            app.Run(new RequestDelegate(Greet));

        }

        private async Task Greet(HttpContext context)
        {
            await context.Response.WriteAsync($"Hello World to {context.Request.Path} ");
        }
        private async Task PrintUrls(HttpContext context, Dictionary<string, int> stats)
        {
            foreach (var url in stats)
            {
                await context.Response.WriteAsync($"Url:{url.Key} Visited Count {url.Value} <br>");
            }
        }
        private void AddUrlsToDictionary(string url, string message)
        {
            if (message == "Found")
            {
                UrlsAddingorUpdating(StatUrlsCount, url);
            }
            else
            {
                UrlsAddingorUpdating(NotFoundUrls, url);
            }
        }

        private void UrlsAddingorUpdating(Dictionary<string, int> statsUrls, string url)
        {
            if (statsUrls.ContainsKey(url))
            {
                statsUrls[url] += 1;
            }
            else
            {
                statsUrls.Add(url, 1);
            }
        }
    }
}
