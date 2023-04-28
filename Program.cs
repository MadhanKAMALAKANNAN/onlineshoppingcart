/* Author: Madhan KAMALAKANNAN  Madhan.KAMALAKANNAN@outlook.com
 Created : Created 29/Aug/2021
 Modified:  /Dec/2021  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineShoppingCart;

namespace OnlineShoppingCart
{
    public class Program
    {
        public static void Main(string[] args)
        {
             CreateHostBuilder(args).Build().Run();
           // var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //new Startup(builder);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                { 
                       webBuilder.UseStartup<Startup>();
                });

       


    }
}
