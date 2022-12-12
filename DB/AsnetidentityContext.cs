using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace OnlineShoppingCart.DB
{
    public class AsnetidentityContext : IdentityDbContext<IdentityUser,IdentityRole, string> //DbContext
    {

        protected IConfiguration configuration;

 
        public AsnetidentityContext(DbContextOptions<AsnetidentityContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ActivationCode> ActivationCode { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Order { get; set; }
        public DbSet<ProductReview> ProductReview { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<ShippingOptions> ShippingOptions { get; set; }

        public DbSet<Coupon> Coupon { get; set; }
        public virtual DbSet<ProductCategories> ProductCategories { get; set; }

        //------------------------------------
    }
}
