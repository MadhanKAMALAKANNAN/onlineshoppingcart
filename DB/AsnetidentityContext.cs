/* Author: Madhan KAMALAKANNAN  , Madhan.KAMALAKANNAN@outlook.com ,  Madhan.KAMALAKANNAN@gmail.com   
Created : Created 29/Aug/2021
Modified:  /Dec/2021  
*/

using OnlineShoppingCart.Models;
 
using Microsoft.EntityFrameworkCore;
 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace OnlineShoppingCart.DB
{
    public partial class AsnetidentityContext : IdentityDbContext<IdentityUser> // ApiAuthorizationDbContext<IdentityUser>
    {

        public AsnetidentityContext(DbContextOptions<AsnetidentityContext> options)
           : base(options)
        {
        }
        /*
        public AsnetidentityContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
             : base(options, operationalStoreOptions)
        {

        }
       
        public virtual DbSet<ActivationCode> ActivationCode { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; } */
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartDetails> CartDetails { get; set; }
        public virtual DbSet<Coupon> Coupon { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<PaymentTypes> PaymentTypes { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategories> ProductCategories { get; set; }
        public virtual DbSet<ProductReview> ProductReview { get; set; }
        public virtual DbSet<ShippingOptions> ShippingOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*
            modelBuilder
              .UseIdentityColumns()
              .HasAnnotation("Relational:MaxIdentifierLength", 128)
              .HasAnnotation("ProductVersion", "5.0.0-rc.1.20417.2");

            modelBuilder.Entity("FormsAuthentication.Models.IdentityUser", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<int>("AccessFailedCount")
                    .HasColumnType("int");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Email")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<bool>("EmailConfirmed")
                    .HasColumnType("bit");

                b.Property<bool>("LockoutEnabled")
                    .HasColumnType("bit");

                b.Property<DateTimeOffset?>("LockoutEnd")
                    .HasColumnType("datetimeoffset");

                b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("PasswordHash")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("PhoneNumber")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("PhoneNumberConfirmed")
                    .HasColumnType("bit");

                b.Property<string>("SecurityStamp")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("TwoFactorEnabled")
                    .HasColumnType("bit");

                b.Property<string>("UserName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasDatabaseName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasDatabaseName("UserNameIndex")
                    .HasFilter("[NormalizedUserName] IS NOT NULL");

                b.ToTable("AspNetUsers");
            });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes", b =>
            {
                b.Property<string>("UserCode")
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<string>("ClientId")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<DateTime>("CreationTime")
                    .HasColumnType("datetime2");

                b.Property<string>("Data")
                    .IsRequired()
                    .HasMaxLength(51120)
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Description")
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<string>("DeviceCode")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<DateTime?>("Expiration")
                    .IsRequired()
                    .HasColumnType("datetime2");

                b.Property<string>("SessionId")
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("SubjectId")
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.HasKey("UserCode");

                b.HasIndex("DeviceCode")
                    .IsUnique();

                b.HasIndex("Expiration");

                b.ToTable("DeviceCodes");
            });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.Key", b =>
            {
                b.Property<string>("Id")
                    .HasMaxLength(450)
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("Algorithm")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<DateTime>("Created")
                    .HasColumnType("datetime2");

                b.Property<string>("Data")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)")
                    .HasMaxLength(51120);

                b.Property<bool>("DataProtected")
                    .HasColumnType("bit");

                b.Property<bool>("IsX509Certificate")
                    .HasColumnType("bit");

                b.Property<string>("Use")
                    .HasMaxLength(450)
                    .HasColumnType("nvarchar(450)");

                b.Property<int>("Version")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("Use");

                b.ToTable("Keys");
            });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.PersistedGrant", b =>
            {
                b.Property<string>("Key")
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<string>("ClientId")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<DateTime?>("ConsumedTime")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("CreationTime")
                    .HasColumnType("datetime2");

                b.Property<string>("Data")
                    .IsRequired()
                    .HasMaxLength(51120)
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Description")
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<DateTime?>("Expiration")
                    .HasColumnType("datetime2");

                b.Property<string>("SessionId")
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("SubjectId")
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<string>("Type")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.HasKey("Key");

                b.HasIndex("Expiration");

                b.HasIndex("SubjectId", "ClientId", "Type");

                b.HasIndex("SubjectId", "SessionId", "Type");

                b.ToTable("PersistedGrants");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("NormalizedName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasDatabaseName("RoleNameIndex")
                    .HasFilter("[NormalizedName] IS NOT NULL");

                b.ToTable("AspNetRoles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .UseIdentityColumn();

                b.Property<string>("ClaimType")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ClaimValue")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("RoleId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .UseIdentityColumn();

                b.Property<string>("ClaimType")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ClaimValue")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.Property<string>("LoginProvider")
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("ProviderKey")
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("ProviderDisplayName")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                //b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins").HasKey("LoginProvider", "ProviderKey");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("RoleId")
                    .HasColumnType("nvarchar(450)");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.ToTable("AspNetUserRoles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("LoginProvider")
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("Name")
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("Value")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.HasOne("FormsAuthentication.Models.IdentityUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.HasOne("FormsAuthentication.Models.IdentityUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("FormsAuthentication.Models.IdentityUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.HasOne("FormsAuthentication.Models.IdentityUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
         

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CartDetails>(entity =>
            {
                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartDetails_Cart");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("FK_CartDetails_Coupon1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartDetails_Product");

                entity.HasOne(d => d.ShippingOption)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.ShippingOptionId)
                    .HasConstraintName("FK_CartDetails_ShippingOptions");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.CouponName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CouponValue).HasColumnType("money");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetails_Orders");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Payment1)
                    .HasColumnType("money")
                    .HasColumnName("Payment");

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK_Payment_Cart");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Payment_Orders");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .HasConstraintName("FK_Payment_PaymentTypes");
            });

            modelBuilder.Entity<PaymentTypes>(entity =>
            {
                entity.Property(e => e.PaymentType).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ActivationCodes)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(6, 0)");

                entity.Property(e => e.ProductPic)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ShowtoSale)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProductCategories)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductCategoriesId)
                    .HasConstraintName("FK_Product_ProductCategories");
            });

            modelBuilder.Entity<ProductCategories>(entity =>
            {
                entity.Property(e => e.CategoryDesc).HasMaxLength(210);

                entity.Property(e => e.CategoryName).HasMaxLength(120);

                entity.Property(e => e.NavUrl).HasMaxLength(510);
            });

            modelBuilder.Entity<ProductReview>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Rating)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Review)
                    .HasMaxLength(800)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductReview)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductReview_Product");
            });

            modelBuilder.Entity<ShippingOptions>(entity =>
            {
                entity.Property(e => e.ShippingCost).HasColumnType("money");

                entity.Property(e => e.ShippingDestinationCountry).HasMaxLength(150);

                entity.Property(e => e.ShippingDestinationPostCode)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ShippingDestinationState).HasMaxLength(150);

                entity.Property(e => e.ShippingOption).HasMaxLength(50);

                entity.Property(e => e.ShippingOptionDesc)
                    .HasMaxLength(150)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);

            base.OnModelCreating(modelBuilder);
            */
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
    
}