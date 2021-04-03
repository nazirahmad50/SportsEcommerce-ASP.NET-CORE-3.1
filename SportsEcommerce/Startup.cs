using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEcommerce
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            // Setup DB to the type of database it will connect, and which connection string will descibe that connection
            services.AddDbContext<ApplicationDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:SportsEcommerceConnection"]);
            });

            // Scoped services that creates a service where each HTTP request gets its own repository object,
            // which is the way that entity framework core is typically used
            services.AddScoped<IStoreRepository, EFStoreRepository>();
            services.AddScoped<IOrderRepository, EFOrderRepository>();


            services.AddRazorPages().AddRazorRuntimeCompilation();

            // sets up in-memory data store so sessions can be stored in memory,
            // this means that sessions are lost after applicaiton is stopped or restarted 
            services.AddDistributedMemoryCache();
            services.AddSession();
            // request for for the Cart service will be handled by creating SessionCart objects
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("catPage", "{category}/Page{productPage}",
                    new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute("page", "Page{productPage:int}",
                    new { controller = "Home", action = "Index", productPage = 1 });

                endpoints.MapControllerRoute("category", "{category}",
                   new { controller = "Home", action = "Index", productPage = 1 });

                endpoints.MapControllerRoute("Pagination", "Products/Page{productPage}",
                    new { controller = "Home", action = "Index" });
                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
