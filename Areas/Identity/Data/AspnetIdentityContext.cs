using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.Models;

namespace OnlineShoppingCart.Data
{
    public class AsnetidentityContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public AsnetidentityContext(DbContextOptions<AsnetidentityContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<VisitDetails> VisitDetails { get; set; }
        ////public virtual DbSet<AspNetRoles> AspNetRoles { get; set; } 
        ////public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<VisitDetails>(entity =>
        //    {
        //        entity.Property(e => e.Id)
        //            .HasColumnName("id")
        //            .ValueGeneratedOnAdd();

        //        entity.Property(e => e.Count).HasColumnName("count");

        //        entity.Property(e => e.UserDetails).HasMaxLength(250);
        //    });
        //modelBuilder.Entity<AspNetRoles>(entity =>
        //{
        //    entity.HasIndex(e => e.NormalizedName)
        //        .HasName("RoleNameIndex")
        //        .IsUnique()
        //        .HasFilter("([NormalizedName] IS NOT NULL)");

        //    entity.Property(e => e.Name).HasMaxLength(256);

        //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
        //});
        //modelBuilder.Entity<AspNetUserRoles>(entity =>
        //{
        //    entity.HasKey(e => new { e.UserId, e.RoleId });

        //    entity.HasIndex(e => e.RoleId);

        //    //entity.HasOne(d => d.Role)
        //    //    .WithMany(p => p.AspNetUserRoles)
        //    //    .HasForeignKey(d => d.RoleId);

        //    entity.HasOne(d => d.User)
        //        .WithMany(p => p.AspNetUserRoles)
        //        .HasForeignKey(d => d.UserId);
        //});
        //modelBuilder.Entity<AspNetUserLogins>(entity =>
        //{
        //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

        //    entity.HasIndex(e => e.UserId);

        //    entity.Property(e => e.LoginProvider).HasMaxLength(128);

        //    entity.Property(e => e.ProviderKey).HasMaxLength(128);

        //    entity.Property(e => e.UserId).IsRequired();

        //    entity.HasOne(d => d.User)
        //        .WithMany(p => p.AspNetUserLogins)
        //        .HasForeignKey(d => d.UserId);
        //});
        // base.OnModelCreating(modelBuilder);

        // }

        //public DbSet<OnlineShoppingCart.Models.VisitDetails> VisitDetails { get; set; }
    }
}
