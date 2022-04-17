using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConceptArchitect.BookManagement;
using ConceptArchitect.BookManagement.Repositories.BookStoreRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BooksWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration,IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        private IWebHostEnvironment env;





        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            string path = Path.Join(env.ContentRootPath, "Data","books.db");

            services.AddSingleton<IStoreSerializer, FlatFileBookSerializer>(provider => new FlatFileBookSerializer(path));
          
            services.AddSingleton<IAuthorRepository, BookStoreAuthorRepository>();
            services.AddSingleton<IAuthorService, PersistentAuthorService>();
            services.AddSingleton<IBookRepository, BookStoreBookRepository>();

            services.AddSingleton<IBookService, PersistentBookService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Author}/{action=Create}/{id?}");
            });
        }
    }
}
