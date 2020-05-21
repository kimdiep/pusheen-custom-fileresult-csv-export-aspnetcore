using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using PusheenCustomExportCsv.Web.Data;
using PusheenCustomExportCsv.Web.Services;

namespace PusheenCustomExportCsv
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _Environment = environment;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _Environment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_Environment.IsDevelopment())
            {
                services.AddDbContext<PusheenCustomExportCsvContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("PusheenCustomExportCsvContext")));

            }
            else 
            {
                services.AddDbContext<PusheenCustomExportCsvContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PusheenCustomExportCsvContext")));

            }

            services.AddControllersWithViews();
            services.AddScoped<IPusheenService, PusheenService>();

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

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
        }
    }
}
