using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.EntityFrameworkCore;
using RIDER_33129_1.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RIDER_33129_2.Properties;

namespace RIDER_33129_1
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews()
                .ConfigureApplicationPartManager(d =>
                {
                    var related = RelatedAssemblyAttribute.GetRelatedAssemblies(typeof(Constants).Assembly, true);
                    d.ApplicationParts.Add(new AssemblyPart(typeof(Constants).Assembly));
                    if (related.FirstOrDefault() is var match && match != null)
                    {
                        d.ApplicationParts.Add(new CompiledRazorAssemblyPart(match));
                        var compiledItems = match.ExportedTypes.Where(d => d.IsDefined(typeof(RazorCompiledItem), false));
                        foreach (var compiledItem in compiledItems)
                        {
                            var attr = compiledItem.GetCustomAttribute<RazorCompiledItemAttribute>();
                        }
                    }
                });
            services.AddRazorPages().AddApplicationPart(typeof(Constants).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapAreaControllerRoute(
                    "defaultArea", 
                    "MyFeature", 
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages();
            });
        }
    }
}