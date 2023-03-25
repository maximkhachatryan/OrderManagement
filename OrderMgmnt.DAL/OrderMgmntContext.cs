﻿using Microsoft.EntityFrameworkCore;
using OrderMgmnt.DAL.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.ClientName).IsRequired();
                entity.Property(e => e.ClientAddress).IsRequired();
                entity.Property(e => e.ClientPhoneNumber).IsRequired();
                entity.HasOne(d => d.Vender)
                  .WithMany(p => p.Orders);
            });

            modelBuilder.Entity<Vender>(entity =>
            {
                entity.Property(e => e.PhoneNumber1).IsRequired();
                entity.Property(e => e.BrandName).IsRequired();
            });
        }
    }
}