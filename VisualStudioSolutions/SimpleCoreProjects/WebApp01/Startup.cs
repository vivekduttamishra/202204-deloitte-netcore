using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApp01.Middlewares;
using WebApp01.Services;
using WebApp01.Authentication;

namespace WebApp01
{
    public class Startup
    {
        
       
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<SimpleGreetService>(); //only one object will be created before its first use.

            services.AddSingleton<TimeUtil>();

            // services.AddSingleton<IGreetService, TimedGreetService>();

            services.AddSingleton<IGreetService, ConfigurableGreetServiceV3>();

            services.AddSingleton<IUrlStatsService,InMemoryUrlStatsService>();

            services.AddAuthenticationService();

            services.AddControllersWithViews(); //enable MVC services
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                                IWebHostEnvironment env,
                                ILogger<Startup> logger,
                                IGreetService greetService                                
                                )
        {

            

            logger.LogInformation($"Current Environment is '{env.EnvironmentName}'");

            app.UsePeformanceLogger();
            app.UseUserAuthentication();

            app.UseStats(); //configures two middlewares

            //app.UseMiddleware<StaticFileMiddleware>();
            app.UseStaticFileMiddleware();

            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }





            


            if (env.EnvironmentName == "HarryPotter")
            {
                app.UseOnUrl("/hogwards", async context =>
                {
                    await context.Response.WriteAsync($"Welcome to Hogward school of wizard and witchcraft:" +
                        $" ${context.Request.Path.Value.Replace("/", " ")}");
                }, config=>config.MatchType=MatchType.Contains);

            }
            //else
            //{
            //    app.UseOnUrl("/hogwards", async context =>
            //    {
            //        context.Response.StatusCode = 403;
            //        await context.Response.WriteAsync($"Muggles are not allowed at Hogwards");
            //    });
            //}

            app.UseRouting();
            app.UseEndpoints(config =>
            {
                config.MapControllerRoute("DefaultRoute",
                        "{controller=Greet}/{action=Welcome}/{id?}"
                    );
            });

            app.UseProtectedRoute("/profile", async context =>
            {
                var user = context.Request.Headers["UserName"];
                await context.Response.WriteAsync($"<h1>Welcome {user}</h1>");

            });

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

                for(int i=0;i<5;i++)
                {
                    var service = context.RequestServices.GetService<IGreetService>(); //get the service object from the provider

                    var message = service.Greet(name);

                    await context.Response.WriteAsync($"<p>{message}</p>");
                }
                

            }, opt => opt.MatchType = MatchType.StartsWith);


            app.UseOnUrl("/greet1", async context =>
            {
                var name = context.Request.Path.Value.Split("/")[2];

                SimpleGreetService service=context.RequestServices.GetService<SimpleGreetService>(); //get the service object from the provider

                var message = service.Greet(name);

                await context.Response.WriteAsync(message);

            }, opt=>opt.MatchType=MatchType.StartsWith);

            app.UseStaticFiles();

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


            app.UseOnUrl("/books", async context =>{
                await context.Response.WriteAsync("Getting a List of books");
            });

            app.UseOnUrl("/books", async context => {
                var fragments = context.Request.Path.Value.Split('/');
                var id = fragments[2];
                await context.Response.WriteAsync($"Getting Info about a specific book with id {id}");
            }, opt=> opt.MatchType=MatchType.StartsWith);

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

            app.UseOnUrl("/time", async context =>
            {
                  await context.Response.WriteAsync($"Time now is {DateTime.Now.ToLongTimeString()}");

            }, opt=> opt.MatchType=MatchType.Contains);


            app.Use (next => async context =>
            {
                //this middleware just logs the information
                Console.WriteLine($"Received {context.Request.Method} {context.Request.Path} " );
                //there is not visible output here.
                await next(context);  //pass control to the next middleware
            });


            // all routes below this point should be protected
            app.UseProtectAll();

            app.UseOnUrl("/admin", async context =>
            {
                await context.Response.WriteAsync("<h1>Admin Page</h1>");
            });

            app.UseOnUrl("/secret", async context =>
            {
                await context.Response.WriteAsync("<h1>Secret Page</h1>");
            });

            app.UseOnUrl("/orders", async context =>
            {
                await context.Response.WriteAsync("<h1>Orders Page</h1>");
            });


        }

        public RequestDelegate Greeter (RequestDelegate next)
        {
            return Greet;
        }


        private async Task Greet(HttpContext context)
        {
            await context.Response.WriteAsync($"Hello World to {context.Request.Path} ");
        }
    }
}
