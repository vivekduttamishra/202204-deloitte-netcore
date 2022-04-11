using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApp01
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        static void HandleRequest()
        {

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //  app.Use(next => HandleRequest);
            //{
            //if (context.Request.Path.Value.Contains("/time"))
            //{
            //    await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

            //}
            //else
            //{
            //    await next(context); 
            //}
            // });

            

            app.Use(next => async context =>
            {
                await context.Response.WriteAsync($"<h1>Books:</h1> <hr/>");//header
                DateTime dt = DateTime.Now;
                await next(context);
                DateTime dt2 = DateTime.Now;
                TimeSpan ts = (dt2 - dt);

                await context.Response.WriteAsync($"<br/><hr/>Time taken: {ts.TotalSeconds}<hr/><br/><h6>Copyrights: </h6>"); //Footer
            });

            app.Use(next => async context =>
            {
                if (context.Request.Path.Value.Contains("/long-running"))
                {
                    await context.Response.WriteAsync($"long running started");
                    await Task.Delay(1000);
                    await context.Response.WriteAsync($"<br/>long running ended");
                }
                else
                {
                    await next(context); //I don't handle this url. let someone else handle it. I don't care.
                }

            });

            app.UseOnURL("/date", async context =>
            {
                await context.Response.WriteAsync($"Date is : {DateTime.Now.ToLongDateString()}");
            });

            Middleware.UseOnURL(app, "/time", async context =>
            {
                await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

            });
            //OR
            //app.Use(next => async context =>
            //{
            //    if (context.Request.Path.Value.Contains("/time"))
            //    {
            //        await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

            //    }
            //    else
            //    {
            //        await next(context);
            //    }
            //});

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

            app.Use(next => async context =>
            {
                //this middleware just logs the information
                Console.WriteLine($"Received {context.Request.Method} {context.Request.Path} ");
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
