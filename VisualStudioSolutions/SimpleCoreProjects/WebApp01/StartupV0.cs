using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace WebApp01
{
    public class StartupV0
    {
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


            app.Use(next => async context =>
            {
                if (context.Request.Path.Value.Contains("/long-running"))
                {
                    await context.Response.WriteAsync("Long running The work started...");
                    await Task.Delay(2000);
                    await context.Response.WriteAsync("Long running workd finished...");

                }
                else
                {
                    await next(context); //I don't handle this url. let someone else handle it. I don't care.
                }
            });


            app.Use(next => async context =>
            {
                if(context.Request.Path.Value.Contains("/time"))
                {
                    await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

                }else
                {
                    await next(context); //I don't handle this url. let someone else handle it. I don't care.
                }
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
    }
}
