/* Author: Madhan KAMALAKANNAN  
 Created : Created 29/Aug/2021
 Modified: /Dec/2021  
 Contact : madhan.kamalakannan@gmail.com  or madhan.kamalakannan@outlook.com
 */

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
using OnlineShoppingCart.DB;
 
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
 
using System.Collections;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
 
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using OnlineShoppingCart.Models;
using Microsoft.AspNetCore.Authentication;

namespace OnlineShoppingCart
{
    public class Startup
    {
        public IEnumerable<KeyValuePair<string, string>> kvPList = null;  
        public KeyValuePair<string, string>[] arrayKP = null; 

       /* public Startup(WebApplicationBuilder builder)//IConfiguration configuration)
        {
            Configuration = builder.Configuration;
            kvPList =  Configuration.AsEnumerable();
            arrayKP = kvPList.ToArray();
            ConfigureServices1(builder.Services);
            Configure1(builder.Build(), builder.Environment);
        }
       */
        public Startup(IConfiguration configuration)
        {
            Configuration =configuration;
            kvPList = Configuration.AsEnumerable();
            arrayKP = kvPList.ToArray();
            
        }

        // public ConfigurationManager Configuration { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string constringName = "AspNetNZDemosContextConnection";
            string hosturl = Configuration["urls"] + "";
            if (hosturl.ToLower().Contains("127.0.0.1") || hosturl.ToLower().Contains("localhost"))
            {
                constringName = "AspNetNZDemosContextConnectionLocal";
            }

            
            services.AddControllersWithViews();//.AddRazorRuntimeCompilation();
           
            var connetionString = Configuration.GetConnectionString(constringName);
            services.AddDbContext<AsnetidentityContext>(options => options.UseSqlServer(connetionString));
           // services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AsnetidentityContext>();

     

            
           // services.AddDbContext<AsnetidentityContext>(opt => { opt.UseSqlServer(connetionString, o => o.EnableRetryOnFailure()); });

            //services.AddDefaultIdentity<IdentityUser>(options => { options.SignIn.RequireConfirmedAccount = true;}).AddRoles<IdentityRole>().AddEntityFrameworkStores<AsnetidentityContext>();
     
             


            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();  
                options.AddPolicy("SiteAdmin", policy => policy.RequireRole("Admin"));
                
            });

            services.AddRazorPages(options =>
            {
               options.Conventions.AuthorizeAreaFolder("Identity", "/Manage", "SiteAdmin");
            }).AddRazorRuntimeCompilation();
            //services.AddApplicationInsightsTelemetry();// AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)//, AsnetidentityContext dbcontext)
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
            
            app.UseAuthentication();
            app.UseAuthorization();
            
           // app.UseSession();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(name: "default", pattern: "/identity/account/login");

                endpoints.MapControllers();

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Login}/{id?}");

                endpoints.MapControllerRoute(
                   name: "Admin",
                   pattern: "Admin/{controller=Products}/{action=Index}/{id?}");


                endpoints.MapRazorPages().RequireAuthorization();

                endpoints.MapDefaultControllerRoute().RequireAuthorization();

            });
 
   
        }
    }
}
