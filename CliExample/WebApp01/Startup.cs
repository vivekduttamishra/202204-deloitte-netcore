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

namespace WebApp01
{
    public class Startup
    {
        DataTable dt = new DataTable();
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


          

            app.UseOnUrl("/stat", async context =>
        {
            //await context.Response.WriteAsync("404");
            string existurl = "";
            foreach (DataRow dr in dt.Rows)
            {
                existurl = dr["URL"].ToString();
                if (!existurl.Contains("404"))
                {
                    existurl = dr["Url"].ToString() + " " + dr["Count"].ToString();
                    await context.Response.WriteAsync(existurl);

                }
                //await context.Response.WriteAsync("Long running The work started...");
                //await Task.Delay(2000);
                //await context.Response.WriteAsync("Long running workd finished...");
            }

        });
            app.Use(next => async context =>
            {
                AddandIncrementUrl(context.Request.Path.Value.ToString(), 1);
                if (dt.Rows.Count > 1)
                {
                    await context.Response.WriteAsync(dt.Rows[1]["Url"].ToString() + " " + dt.Rows[1]["Count"].ToString());
                }
                await next(context);
            });
            //app.UseOnUrl("/404", async context =>
            //{
            //    await context.Response.WriteAsync("404");
            //    string existurl = "";
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        existurl = dr["Url"].ToString();
            //        if (existurl.Contains("404"))
            //        {
            //            existurl = dr["Url"].ToString() + " " + dr["Count"].ToString();
            //            await context.Response.WriteAsync(existurl);

            //        }
            //        //await context.Response.WriteAsync("Long running The work started...");
            //        //await Task.Delay(2000);
            //        //await context.Response.WriteAsync("Long running workd finished...");
            //    }
            //});

            //Middlewares.UseOnUrl(app, "/time", async context =>
            //{
            //    await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

            //});

            //DateTime dt;
            //TimeSpan ts;
            //app.Use(next => async context =>
            //{

            //    if (context.Request.Path.Value.Contains("/long-running"))
            //    {
            //       dt = DateTime.Now;
            //        await context.Response.WriteAsync($"Long running has strated{dt}");
            //        await Task.Delay(2000);
            //        ts = dt.Subtract(DateTime.Now);
            //        await context.Response.WriteAsync($"<br/>Long running has finished{ts}");
            //    }
            //    else if (context.Request.Path.Value.Contains("/footer"))
            //    {
            //        await context.Response.WriteAsync($"<h1>Welcome to the Header</h1><hr/>");
            //        await context.Response.WriteAsync($"Handled {context.Request.Path}");
            //        await context.Response.WriteAsync($"<hr/>Footer content's Web");

            //    }
            //    else
            //    {
            //        await next(context); //I don't handle this url. let someone else handle it. I don't care.
            //    }
            //});


            //app.Use(next => async context =>
            //{
            //    //display a common message
            //    await context.Response.WriteAsync($"<h1>Book's Web</h1><hr/>");
            //    //let other middleware do whatever they want.
            //    await next(context);  //pass control to the next middleware
            //});


            //app.Use(next => async context =>
            //{
            //    //this middleware just logs the information
            //    Console.WriteLine($"Received {context.Request.Method} {context.Request.Path} ");
            //    //there is not visible output here.
            //    await next(context);  //pass control to the next middleware
            //});


            app.Use(next => async context =>
            {
                //display a common message
                await context.Response.WriteAsync($"<hr/><h1>Footer Content</h1>");
                //let other middleware do whatever they want.
               // await next(context);  //pass control to the next middleware
            });


            //app.Use(next =>
            //{

            //    RequestDelegate thisMiddleware = async context =>
            //    {
            //        //perform whatever you want to
            //        await context.Response.WriteAsync($"Handled {context.Request.Path}");
            //        //await context.Response.WriteAsync($"<hr/><h1>Footer Content</h1>");
            //    };
            //   // await context.Response.WriteAsync($"<h1>Book's Web</h1><hr/>");
            //    return thisMiddleware;
            //});


            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync($"Time: {DateTime.Now.ToLongTimeString()}");
            //});


            //unreachable code
           // app.Run(new RequestDelegate(Greet));

        }

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
        private async Task Greet(HttpContext context)
        {
            await context.Response.WriteAsync($"Hello World to {context.Request.Path} ");
        }

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
