using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConceptArchitect.BookManagement;
using ConceptArchitect.BookManagement.Repositories.BookStoreRepository;
using ConceptArchitect.BookManagement.Repositories.EFRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

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
            services.AddRazorPages();
                        
            services
                .AddAuthentication("CookieAuth")
                
                .AddCookie("CookieAuth", config =>
                {
                    config.Cookie.Name = "Buscuit";
                    config.LoginPath = "/User/Login";
                });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer=true,
                        ValidateAudience=true,
                        ValidAudience=Configuration["Jwt:Audience"],
                        ValidIssuer=Configuration["Jwt:Issuer"],
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };

                });

            var dbPath = Path.Join(env.ContentRootPath, "App_Data");
            //var connectionString = @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True;Connect Timeout=30";

            var connectionString = Configuration["ConnectionStrings:BooksOld"].Replace("[DataDirectory]", dbPath);
            services.AddSingleton<IBookRepository, AdoNetBookRepository>(provider => new AdoNetBookRepository(connectionString));

            //ConfigureFlatFileRepository(services);
            ConfigureEFREpository(services);

            services.AddTransient<IAuthorService, PersistentAuthorService>();
            services.AddTransient<IUserManager, PersistentUserManager>();
            services.AddTransient<DataSeeder>();
        }

        private static void ConfigureEFREpository(IServiceCollection services)
        {
            services.AddDbContext<BMSContext>(); //EF Context added
            services.AddTransient<IAuthorRepository, EFAuthorRepository>();
            services.AddTransient<IUserRepository, EFUserRepository>();
            
        }

        private void ConfigureFlatFileRepository(IServiceCollection services)
        {
            //string path = Path.Join(env.ContentRootPath, "Data","books.db");
            //services.AddSingleton<IStoreSerializer, FlatFileBookSerializer>(provider => new FlatFileBookSerializer(path));

            string path = Path.Join(env.ContentRootPath, "Data", "books.json");
            services.AddSingleton<IStoreSerializer, JsonFileBookSerializer>(provider => new JsonFileBookSerializer(path));
            services.AddSingleton<IAuthorRepository, BookStoreAuthorRepository>();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Author}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
