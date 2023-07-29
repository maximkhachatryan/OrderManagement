using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OrderMgmnt.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgmnt.DAL
{
    public class OrderMgmntContext : DbContext
    {
        public OrderMgmntContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VendorAddress> VendorAddresses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.ProductDescription).IsRequired();
                entity.Property(b => b.ProductPrice).HasPrecision(10, 2);
                entity.HasOne(d => d.VendorAddress)
                  .WithMany(p => p.Orders).IsRequired();

                entity.Property(e => e.OrderCode)
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                    .IsRequired();
                entity.HasIndex(e => e.OrderCode).IsUnique().IsClustered(false);

            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.Property(e => e.PhoneNumber1).IsRequired();
                entity.Property(e => e.BrandName).IsRequired();

                entity.Property(e => e.VendorWalletAmount).HasPrecision(12, 2);
                entity.HasMany(e => e.Addresses).WithOne(a => a.Vendor).IsRequired();

                entity.Property(e => e.Code)
                    .IsUnicode(false)
                    .IsRequired()
                    .HasDefaultValue("")
                    .HasMaxLength(6);
                entity.HasIndex(e => e.Code).IsUnique();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserName).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.HasIndex(e => e.UserName);
                entity.HasOne(e => e.Vendor).WithMany().IsRequired();
            });

            modelBuilder.Entity<VendorAddress>(entity =>
            {
                entity.Property(e => e.AddressInfo).IsRequired();
            });
        }
    }
}
