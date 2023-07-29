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
        public virtual DbSet<Vender> Venders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VenderAddress> VenderAddresses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.ProductDescription).IsRequired();
                entity.Property(b => b.ProductPrice).HasPrecision(10, 2);
                entity.HasOne(d => d.VenderAddress)
                  .WithMany(p => p.Orders).IsRequired();

                entity.Property(e => e.OrderCode)
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                    .IsRequired();
                entity.HasIndex(e => e.OrderCode).IsUnique().IsClustered(false);

            });

            modelBuilder.Entity<Vender>(entity =>
            {
                entity.Property(e => e.PhoneNumber1).IsRequired();
                entity.Property(e => e.BrandName).IsRequired();

                entity.Property(e => e.VenderWalletAmount).HasPrecision(12, 2);
                entity.HasMany(e => e.Addresses).WithOne(a => a.Vender).IsRequired();

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
                entity.HasOne(e => e.Vender).WithMany().IsRequired();
            });

            modelBuilder.Entity<VenderAddress>(entity =>
            {
                entity.Property(e => e.AddressInfo).IsRequired();
            });
        }
    }
}
