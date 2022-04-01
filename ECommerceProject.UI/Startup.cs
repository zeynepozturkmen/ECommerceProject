using ECommerceProject.Business.IService;
using ECommerceProject.Business.IUnitOfWorks;
using ECommerceProject.Business.Service;
using ECommerceProject.Business.UnitOfWorks;
using ECommerceProject.Data.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceProject.UI
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

            var connectionString = Configuration.GetConnectionString("DefaultConnectionString");

            services.AddDbContext<ECommerceDbContext>(options => options.UseSqlServer(connectionString, optionsBuilder =>
                optionsBuilder.MigrationsAssembly("ECommerceProject.Data")), ServiceLifetime.Scoped
            );


            //sayfa yenilendiginde degisikliklerin algýlanmasý icin
            services.AddControllersWithViews()
         .AddRazorRuntimeCompilation();

            #region Service
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICampaignService, CampaignService>();
            services.AddTransient<IOrderService, OrderService>();
            #endregion

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            ECommerceDbSeed.Initialize(app.ApplicationServices, true);
        }
    }
}
