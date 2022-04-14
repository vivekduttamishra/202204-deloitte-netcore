using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace WebApp01.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class StaticFileMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<StaticFileMiddleware> logger;
        private readonly IWebHostEnvironment host;
        DirectoryInfo staticFileRoot;

        public StaticFileMiddleware(RequestDelegate next,IConfiguration config, IWebHostEnvironment host, ILogger<StaticFileMiddleware> logger)
        {
            this.logger = logger;
            this.host = host;

            var path = config["staticfiles:root"];
            if (string.IsNullOrEmpty(path))
                path = "public";

            staticFileRoot = new DirectoryInfo(Path.Join(host.ContentRootPath, path));
            logger.LogInformation($"Middleware configured for path {staticFileRoot.FullName}");
            _next = next;
            
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path;
            var filePath = Path.Join(staticFileRoot.FullName, path);
            var file = new FileInfo(filePath);

            if(file.Exists)
            {
                httpContext.Response.Headers["content-type"] = $"text/{file.Extension.Substring(1)}";
                //create a read stream over file
                using(var stream = file.OpenRead())
                {
                    

                    await stream.CopyToAsync(httpContext.Response.Body); //copy file to response
                }
            }
            else
                await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class StaticFileMiddlewareExtensions
    {
        public static IApplicationBuilder UseStaticFileMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<StaticFileMiddleware>();
        }
    }
}
