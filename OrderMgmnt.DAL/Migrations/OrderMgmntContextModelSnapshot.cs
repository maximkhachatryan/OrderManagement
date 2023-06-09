﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderMgmnt.DAL;

namespace OrderMgmnt.DAL.Migrations
{
    [DbContext(typeof(OrderMgmntContext))]
    partial class OrderMgmntContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrderMgmnt.DAL.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AcceptDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ActualPickUpDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ClientAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ClientFillDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ClientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ClientPreferredDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ClientRejectDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeliveryStartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DesiredPickUpDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeliveryPaymentByClient")
                        .HasColumnType("bit");

                    b.Property<string>("OtherNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ProductPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime?>("RejectDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SentBackToVenderDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ShouldProductPriceBePaid")
                        .HasColumnType("bit");

                    b.Property<Guid>("VenderAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VenderAddressId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OrderMgmnt.DAL.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("VenderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserName");

                    b.HasIndex("VenderId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OrderMgmnt.DAL.Entities.Vender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstagramLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("VenderWalletAmount")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.Property<string>("WebsiteLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Venders");
                });

            modelBuilder.Entity("OrderMgmnt.DAL.Entities.VenderAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("District")
                        .HasColumnType("int");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<Guid>("VenderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VenderId");

                    b.ToTable("VenderAddresses");
                });

            modelBuilder.Entity("OrderMgmnt.DAL.Entities.Order", b =>
                {
                    b.HasOne("OrderMgmnt.DAL.Entities.VenderAddress", "VenderAddress")
                        .WithMany("Orders")
                        .HasForeignKey("VenderAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VenderAddress");
                });

            modelBuilder.Entity("OrderMgmnt.DAL.Entities.User", b =>
                {
                    b.HasOne("OrderMgmnt.DAL.Entities.Vender", "Vender")
                        .WithMany()
                        .HasForeignKey("VenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vender");
                });

            modelBuilder.Entity("OrderMgmnt.DAL.Entities.VenderAddress", b =>
                {
                    b.HasOne("OrderMgmnt.DAL.Entities.Vender", null)
                        .WithMany("Addresses")
                        .HasForeignKey("VenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrderMgmnt.DAL.Entities.Vender", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("OrderMgmnt.DAL.Entities.VenderAddress", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
