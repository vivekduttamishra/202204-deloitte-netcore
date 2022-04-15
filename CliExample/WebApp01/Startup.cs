using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp01.Services;
using WebApp01.MyMiddlewares;
using Microsoft.Extensions.Logging;

namespace WebApp01
{
    public class Startup
    {
        DataTable dt = new DataTable();
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<SimpleGreetService>(); //only one object will be created before its first use.

            services.AddSingleton<TimeUtil>();

            // services.AddSingleton<IGreetService, TimedGreetService>();

            services.AddSingleton<IGreetService, ConfigurableGreetServiceV3>();

            services.AddSingleton<IUrlStatsService, InMemoryUrlStatsService>();
           // services.AddSingleton<IGangishettyService>();
        }
        public void AddandIncrementUrl(string  url, int count)
        {
            if(dt.Rows.Count == 0)
            {
                dt.Columns.Add( "Url",typeof(String));
                dt.Columns.Add("Count", typeof(int));
            }
            int urlcount = 0;
            foreach(DataRow dr in dt.Rows)
            {
                string existurl = dr["Url"].ToString();
                if (existurl.ToLower() == url.ToLower())
                {
                    urlcount = 1;
                    dr["Count"] = Convert.ToInt32(dr["Count"].ToString()) + 1;
                    dt.AcceptChanges();
                }
            }
            if(urlcount == 0)
            {
                dt.Rows.Add(url, count);
            }

        }
        public void Configure(IApplicationBuilder app,
                                IWebHostEnvironment env,
                                ILogger<Startup> logger,
                                IGreetService greetService
                                )
        {

            logger.LogInformation($"Current Environment is '{env.EnvironmentName}'");

            app.UseStats(); //configures two middlewares

            //app.UseDefaultFiles();
           // app.UseFileServer();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }





            if (env.EnvironmentName == "HarryPotter")
            {
                app.UseOnUrl("/hogwards", async context =>
                {
                    await context.Response.WriteAsync($"Welcome to Hogward school of wizard and witchcraft:" +
                        $" ${context.Request.Path.Value.Replace("/", " ")}");
                }, config => config.MatchType = MatchType.Contains);

            }
            //else
            //{
            //    app.UseOnUrl("/hogwards", async context =>
            //    {
            //        context.Response.StatusCode = 403;
            //        await context.Response.WriteAsync($"Muggles are not allowed at Hogwards");
            //    });
            //}


            app.UseOnUrl("/greet4", async context =>
            {
                var name = context.Request.Path.Value.Split("/")[2];

                for (int i = 0; i < 5; i++)
                {

                    var message = greetService.Greet(name);
                    logger.LogInformation($"service #{greetService.GetHashCode()} invoked");
                    await context.Response.WriteAsync($"<p>{message}</p>");
                }


            }, opt => opt.MatchType = MatchType.StartsWith);


            app.UseOnUrl("/greet3", async context =>
            {
                var name = context.Request.Path.Value.Split("/")[2];
                var scope = app.ApplicationServices.CreateScope();
                for (int i = 0; i < 5; i++)
                {

                    var service = scope.ServiceProvider.GetService<IGreetService>();

                    var message = service.Greet(name);

                    await context.Response.WriteAsync($"<p>{message}</p>");
                }


            }, opt => opt.MatchType = MatchType.StartsWith);


            app.UseOnUrl("/greet2", async context =>
            {
                var name = context.Request.Path.Value.Split("/")[2];

                for (int i = 0; i < 5; i++)
                {
                    var service = context.RequestServices.GetService<IGreetService>(); //get the service object from the provider

                    var message = service.Greet(name);

                    await context.Response.WriteAsync($"<p>{message}</p>");
                }


            }, opt => opt.MatchType = MatchType.StartsWith);


            app.UseOnUrl("/greet", async context =>
            {
                var name = context.Request.Path.Value.Split("/")[2];

                SimpleGreetService service = context.RequestServices.GetService<SimpleGreetService>(); //get the service object from the provider

                var message = service.Greet(name);

                await context.Response.WriteAsync(message);

            }, opt => opt.MatchType = MatchType.StartsWith);

            // /hello?name=Vivek
            app.UseOnUrl("/hello", async context =>
            {
                //getting data from query string
                var name = context.Request.Query["name"];

                //creating and using the service
                var service = new SimpleGreetService();
                var message = service.Greet(name);

                //sending the response
                await context.Response.WriteAsync(message);

            });


            app.UseOnUrl("/books", async context => {
                await context.Response.WriteAsync("Getting a List of books");
            });

            app.UseOnUrl("/books", async context => {
                var fragments = context.Request.Path.Value.Split('/');
                var id = fragments[2];
                await context.Response.WriteAsync($"Getting Info about a specific book with id {id}");
            }, opt => opt.MatchType = MatchType.StartsWith);

            app.UseOnUrl("/long-running", async context =>
            {

                await context.Response.WriteAsync("Long running The work started...");
                await Task.Delay(2000);
                await context.Response.WriteAsync("Long running workd finished...");

            });

            app.UseOnUrl("/date", async context =>
            {
                await context.Response.WriteAsync($"Date is : {DateTime.Now.ToLongDateString()}");
            });

            Middlewares.UseOnUrl(app, "/time", async context =>
            {
                await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

            });

            app.Use(next => async context =>
            {
                //this middleware just logs the information
                Console.WriteLine($"Received {context.Request.Method} {context.Request.Path} ");
                //there is not visible output here.
                await next(context);  //pass control to the next middleware
            });


        }

        //public RequestDelegate Greeter(RequestDelegate next)
        //{
        //    return Greet;
        //}


        private async Task Greet(HttpContext context)
        {
            await context.Response.WriteAsync($"Hello World to {context.Request.Path} ");
        }
        //public string ReturnStatValues()
        //{
        //    string result = "";
        //    foreach(DataRow dr in dt.Rows)
        //    {

        //        if (existurl.ToLower() == url.ToLower())
        //        {
        //            urlcount = 1;
        //            dr["Count"] = Convert.ToInt32(dr["Count"].ToString()) + 1;
        //        }
        //    }
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{

        //    if (env.EnvironmentName == "WebApp01")
        //    {
        //        app.UseOnUrl("/hogwards", async context =>
        //        {
        //            await context.Response.WriteAsync($"Welcome to Hogward school of wizard and witchcraft:" +
        //                $" ${context.Request.Path.Value.Replace("/", " ")}");
        //        });

        //    }


        //    app.UseOnUrl("/stat", async context =>
        //{
        //    //await context.Response.WriteAsync("404");
        //    string existurl = "";
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        existurl = dr["URL"].ToString();
        //        if (!existurl.Contains("404"))
        //        {
        //            existurl = dr["Url"].ToString() + " " + dr["Count"].ToString();
        //            await context.Response.WriteAsync(existurl);

        //        }
        //        //await context.Response.WriteAsync("Long running The work started...");
        //        //await Task.Delay(2000);
        //        //await context.Response.WriteAsync("Long running workd finished...");
        //    }

        //});
        //    app.Use(next => async context =>
        //    {
        //        AddandIncrementUrl(context.Request.Path.Value.ToString(), 1);
        //        if (dt.Rows.Count > 1)
        //        {
        //            await context.Response.WriteAsync(dt.Rows[1]["Url"].ToString() + " " + dt.Rows[1]["Count"].ToString());
        //        }
        //        await next(context);
        //    });
        //    //app.UseOnUrl("/404", async context =>
        //    //{
        //    //    await context.Response.WriteAsync("404");
        //    //    string existurl = "";
        //    //    foreach (DataRow dr in dt.Rows)
        //    //    {
        //    //        existurl = dr["Url"].ToString();
        //    //        if (existurl.Contains("404"))
        //    //        {
        //    //            existurl = dr["Url"].ToString() + " " + dr["Count"].ToString();
        //    //            await context.Response.WriteAsync(existurl);

        //    //        }
        //    //        //await context.Response.WriteAsync("Long running The work started...");
        //    //        //await Task.Delay(2000);
        //    //        //await context.Response.WriteAsync("Long running workd finished...");
        //    //    }
        //    //});

        //    //Middlewares.UseOnUrl(app, "/time", async context =>
        //    //{
        //    //    await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

        //    //});

        //    //DateTime dt;
        //    //TimeSpan ts;
        //    //app.Use(next => async context =>
        //    //{

        //    //    if (context.Request.Path.Value.Contains("/long-running"))
        //    //    {
        //    //       dt = DateTime.Now;
        //    //        await context.Response.WriteAsync($"Long running has strated{dt}");
        //    //        await Task.Delay(2000);
        //    //        ts = dt.Subtract(DateTime.Now);
        //    //        await context.Response.WriteAsync($"<br/>Long running has finished{ts}");
        //    //    }
        //    //    else if (context.Request.Path.Value.Contains("/footer"))
        //    //    {
        //    //        await context.Response.WriteAsync($"<h1>Welcome to the Header</h1><hr/>");
        //    //        await context.Response.WriteAsync($"Handled {context.Request.Path}");
        //    //        await context.Response.WriteAsync($"<hr/>Footer content's Web");

        //    //    }
        //    //    else
        //    //    {
        //    //        await next(context); //I don't handle this url. let someone else handle it. I don't care.
        //    //    }
        //    //});


        //    //app.Use(next => async context =>
        //    //{
        //    //    //display a common message
        //    //    await context.Response.WriteAsync($"<h1>Book's Web</h1><hr/>");
        //    //    //let other middleware do whatever they want.
        //    //    await next(context);  //pass control to the next middleware
        //    //});


        //    //app.Use(next => async context =>
        //    //{
        //    //    //this middleware just logs the information
        //    //    Console.WriteLine($"Received {context.Request.Method} {context.Request.Path} ");
        //    //    //there is not visible output here.
        //    //    await next(context);  //pass control to the next middleware
        //    //});


        //    app.Use(next => async context =>
        //    {
        //        //display a common message
        //        await context.Response.WriteAsync($"<hr/><h1>Footer Content</h1>");
        //        //let other middleware do whatever they want.
        //       // await next(context);  //pass control to the next middleware
        //    });


        //    //app.Use(next =>
        //    //{

        //    //    RequestDelegate thisMiddleware = async context =>
        //    //    {
        //    //        //perform whatever you want to
        //    //        await context.Response.WriteAsync($"Handled {context.Request.Path}");
        //    //        //await context.Response.WriteAsync($"<hr/><h1>Footer Content</h1>");
        //    //    };
        //    //   // await context.Response.WriteAsync($"<h1>Book's Web</h1><hr/>");
        //    //    return thisMiddleware;
        //    //});


        //    //app.Run(async context =>
        //    //{
        //    //    await context.Response.WriteAsync($"Time: {DateTime.Now.ToLongTimeString()}");
        //    //});


        //    //unreachable code
        //   // app.Run(new RequestDelegate(Greet));

        //}

        public void displaytext(HttpContext context, int returnthis, IApplicationBuilder app)
        {

            //app.Use(next async context =>
            //{

            //    if (context.Request.Path.Value.Contains("/time"))
            //    {
            //        await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

            //    }
            //    //if (returnthis == 1)
            //    //    return thismiddleware;
            //    //else
            //    await next(context);
            //});
        }
        //public static async Task Greet(HttpContext context)
        //{
        //    await context.Response.WriteAsync($"Hello World to {context.Request.Path} ");
        //}

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

    }
}
