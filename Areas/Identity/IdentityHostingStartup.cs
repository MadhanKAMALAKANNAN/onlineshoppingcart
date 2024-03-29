﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShoppingCart.DB;

[assembly: HostingStartup(typeof(OnlineShoppingCart.Areas.Identity.IdentityHostingStartup))]
namespace OnlineShoppingCart.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<AsnetidentityContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("AsnetidentityContextConnection")));

            //    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<AsnetidentityContext>();
            //});
        }
    }
}