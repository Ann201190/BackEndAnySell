﻿// <auto-generated />
using System;
using BackEndAnySellAccessDataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackEndAnySellDataAccess.Migrations
{
    [DbContext(typeof(CustomDbContext))]
    [Migration("20220520100758_add-store-IsDeleted")]
    partial class addstoreIsDeleted
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackEndAnySellDataAccess.Entities.Discount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DiscountType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("BackEndAnySellDataAccess.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("BackEndAnySellDataAccess.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BackEndAnySellDataAccess.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Count")
                        .HasColumnType("float");

                    b.Property<Guid?>("DiscountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductUnit")
                        .HasColumnType("int");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DiscountId");

                    b.HasIndex("StoreId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BackEndAnySellDataAccess.Entities.ReservationProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Count")
                        .HasColumnType("float");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ReservationProducts");
                });

            modelBuilder.Entity("BackEndAnySellDataAccess.Entities.Store", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<byte[]>("LogoImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("EmployeeStore", b =>
                {
                    b.Property<Guid>("EmployeesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StoresId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EmployeesId", "StoresId");

                    b.HasIndex("StoresId");

                    b.ToTable("EmployeeStore");
                });

            modelBuilder.Entity("BackEndAnySellDataAccess.Entities.Discount", b =>
                {
                    b.HasOne("BackEndAnySellDataAccess.Entities.Store", "Store")
                        .WithMany("Discounts")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("BackEndAnySellDataAccess.Entities.Order", b =>
                {
                    b.HasOne("BackEndAnySellDataAccess.Entities.Store", "Store")
                        .WithMany("Orders")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("BackEndAnySellDataAccess.Entities.Product", b =>
                {
                    b.HasOne("BackEndAnySellDataAccess.Entities.Discount", "Discount")
                        .WithMany("Products")
                        .HasForeignKey("DiscountId");

                    b.HasOne("BackEndAnySellDataAccess.Entities.Store", "Store")
                        .WithMany("Products")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discount");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("BackEndAnySellDataAccess.Entities.ReservationProduct", b =>
                {
                    b.HasOne("BackEndAnySellDataAccess.Entities.Order", "Order")
                        .WithMany("ReservationProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEndAnySellDataAccess.Entities.Product", "Product")
                        .WithMany("ReservationProducts")
                        .HasForeignKey("ProductId");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EmployeeStore", b =>
                {
                    b.HasOne("BackEndAnySellDataAccess.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEndAnySellDataAccess.Entities.Store", null)
                        .WithMany()
                        .HasForeignKey("StoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackEndAnySellDataAccess.Entities.Discount", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("BackEndAnySellDataAccess.Entities.Order", b =>
                {
                    b.Navigation("ReservationProducts");
                });

            modelBuilder.Entity("BackEndAnySellDataAccess.Entities.Product", b =>
                {
                    b.Navigation("ReservationProducts");
                });

            modelBuilder.Entity("BackEndAnySellDataAccess.Entities.Store", b =>
                {
                    b.Navigation("Discounts");

                    b.Navigation("Orders");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
