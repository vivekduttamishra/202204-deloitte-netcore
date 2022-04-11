using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ex1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        private void handleURL(string url, HttpContext context)
        {
            //do something            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            //Create a generic middleware that can execute an action based on a given URL. 
            app.Use(next => async context =>
            {
                await context.Response.WriteAsync("<h1>Example Site</h1></hr>");
                handleURL("test", context);
                await next(context);
            });

            //Create a middleware to add a footer information to all the request 
            app.Use(next => async context =>
            {
                await next(context);
                await context.Response.WriteAsync("<footer>footer section</footer>");                
            });

            //Create a middleware that can log the total time take by current request.
            app.Use(next => async context =>
            {
                await context.Response.WriteAsync($"time take by current request {stopWatch.ElapsedMilliseconds} ms");
                await next(context);
            });
        }
    }
}
