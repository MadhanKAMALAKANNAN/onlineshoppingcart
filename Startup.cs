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

namespace OnlineShoppingCart
{
    public class Startup
    {
        public IEnumerable<KeyValuePair<string, string>> kvPList = null;  
        public KeyValuePair<string, string>[] arrayKP = null; 

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            kvPList =  Configuration.AsEnumerable();
            arrayKP = kvPList.ToArray();
        }

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

           
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSession();//options => { options.IdleTimeout = TimeSpan.FromSeconds(1000000); });
            //services.AddDbContext<AsnetidentityContext>(opt =>
            //opt.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("Dbconn")));
            //services.AddDbContext<AsnetidentityContext>(options => options.UseLazyLoadingProxies().UseMySQL(Configuration.GetConnectionString(constringName)));
            var connetionString = Configuration.GetConnectionString(constringName);


            //services.AddDbContext<AsnetidentityContext>(opt =>
            //opt.UseLazyLoadingProxies().UseSqlServer(connetionString));

            //services.AddDbContext<AsnetidentityContext>(options => options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString)));

            services.AddDbContext<AsnetidentityContext>(opt => { opt.UseSqlServer(connetionString, o => o.EnableRetryOnFailure()); });

            services.AddDefaultIdentity<IdentityUser>(options => { options.SignIn.RequireConfirmedAccount = true;}).AddRoles<IdentityRole>().AddEntityFrameworkStores<AsnetidentityContext>();
            services.AddAuthentication().AddGoogle(options =>
            {
                
                options.ClientId = arrayKP.FirstOrDefault(x => x.Key == "Authentication:Google:ClientId").Value;
                options.ClientSecret = arrayKP.FirstOrDefault(x => x.Key == "Authentication:Google:ClientSecret").Value;
                options.ReturnUrlParameter = "/signin-google";
                options.CallbackPath = "/signin-google";

            });
            //    .AddMicrosoftAccount(options => {

            //    options.ClientId = arrayKP.FirstOrDefault(x => x.Key == "Authentication:Microsoft:ClientId").Value;
            //    options.ClientSecret = arrayKP.FirstOrDefault(x => x.Key == "Authentication:Microsoft:ClientSecret").Value;
            //});


            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();  
                //options.AddPolicy("SiteAdmin", policy => policy.RequireRole("Admin"));
                
            });

            services.AddRazorPages(options =>
            {
               // options.Conventions.AuthorizeAreaFolder("Identity", "/Manage", "SiteAdmin");
            }).AddRazorRuntimeCompilation();
            services.AddApplicationInsightsTelemetry();// AddApplicationInsightsTelemetry();
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

            app.UseSession();
            app.UseEndpoints(endpoints =>
            {

                // endpoints.MapControllerRoute(name: "default", pattern: "/identity/account/login");

                //endpoints.MapControllers();
            
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");

                endpoints.MapControllerRoute(
                   name: "Admin",
                   pattern: "Admin/{controller=Products}/{action=Index}/{id?}");
                //endpoints.MapControllerRoute(
                //  name: "Admina",
                //  pattern: "{controller=Products}/Admin/test1/{action=Index}/{id?}");

                //endpoints.MapControllerRoute(
                // name: "Admina",
                // pattern: "{controller=Products}/Admin/test1/{action=Index}/{id?}");


                ////endpoints.MapControllerRoute(  name: "route for product list", pattern: "Products/Index");

                ////endpoints.MapControllerRoute(
                ////   name: "route for edit qty list",
                ////   pattern: "Cart/EditQty/{json?}");

                endpoints.MapRazorPages().RequireAuthorization();

                endpoints.MapDefaultControllerRoute().RequireAuthorization();

            });
           
            //dbcontext.Database.EnsureDeleted();
            //dbcontext.Database.EnsureCreated();

            //new DBSeeder(dbcontext);
        }
    }
}
