using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackEndAnySellAccessDataAccess.Context
{
    public static class ModelBuilderExtensions   
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Один-ко-многим
            modelBuilder.Entity<ReservationProduct>()
                .HasOne(rp => rp.Product)
                .WithMany(p => p.ReservationProducts);
         
              //  .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Product>()
                .HasOne(p => p.Discount)
                .WithMany(d => d.Products);


            modelBuilder.Entity<Discount>()
                .HasOne(d => d.Store)
                .WithMany(s => s.Discounts);


            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Store)
                .WithMany(s => s.Employees);


            modelBuilder.Entity<Product>()
              .HasOne(p => p.Store)
              .WithMany(s => s.Products);
             
     //            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ReservationProduct>()
              .HasOne(rp => rp.Order)
              .WithMany(o => o.ReservationProducts);


            modelBuilder.Entity<Order>()
              .HasOne(o => o.Store)
              .WithMany(s => s.Orders);

            // уникальность штрих-кода
            /*     modelBuilder.Entity<Product>()
                   .HasIndex(p => p.Barcode)
                   .IsUnique()
                  .HasFilter(null); */

        }
        public static void Initialize(CustomDbContext dbContext)
        {
            var employees1 = new Employee
            { 
                Name = "Ivan",
                Email = "ad669999@gmail.com",
                Role = Role.Manager,
                SurName = "Ivanov",
                StoreId = new Guid("BFBC7481-FB3C-4192-A093-519F40F1B812")
            };

            var employees2 = new Employee
            {
                Name = "Anna",
                Email = "litv@gmail.com",
                Role = Role.Manager,
                SurName = "Litvintceva",
                StoreId = new Guid("BFBC7481-FB3C-4192-A093-519F40F2B822")
            };


            var discont1 = new Discount
            {
                Id = new Guid("BFBC7481-FB3C-4192-A093-519F40F1B811"),
                Name = "EveryDay",
                Value = 25,
                DiscountType = DiscountType.Fixed,
                StoreId = new Guid("BFBC7481-FB3C-4192-A093-519F40F1B812")
            };

            var discont2 = new Discount
            {
                Id = new Guid("BFBC7481-FB3C-4192-A093-519F40F1B888"),
                Name = "Black Fride",
                Value = 20,
                DiscountType = DiscountType.Fixed,
                StoreId = new Guid("BFBC7481-FB3C-4192-A093-519F40F1B812")
            };

            var discont3 = new Discount
            {
                Id = new Guid("BFBC7481-FB3C-4192-A093-519F40F1B777"),
                Name = "Black Fride",
                Value = 10,
                DiscountType = DiscountType.Fixed,
                StoreId = new Guid("BFBC7481-FB3C-4192-A093-519F40F2B822")
            };


            var store1 = new Store
             {
                Id = new Guid("BFBC7481-FB3C-4192-A093-519F40F1B812"),
                Name = "FIRST",
                Employees  = new List<Employee> { employees1 },
                Discounts = new List<Discount> { discont1 }
             };

            var store2 = new Store
            {
                Id = new Guid("BFBC7481-FB3C-4192-A093-519F40F2B822"),
                Name = "SECOND",
                Employees = new List<Employee> { employees2 },
                Discounts = new List<Discount> { discont1 }
            };

            var product1 = new Product
            {
                Id = new Guid("0A325D54-1034-4143-BFE1-4AACE85F1996"),
                Name = "Шоколад",
                Price = 30.50m,
                Discount = discont1,
                Barcode ="1236559565625",
                Count = 4,
                ProductUnit = ProductUnit.Piece, 
                StoreId = new Guid("BFBC7481-FB3C-4192-A093-519F40F1B812"),
                DiscountId = new Guid("BFBC7481-FB3C-4192-A093-519F40F1B811"),               
            };


            //заполнение базы данных
        
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(product1);
            }

            if (!dbContext.Employees.Any())
            {
                dbContext.Employees.Add(employees1);
                dbContext.Employees.Add(employees2);
            }

            if (!dbContext.Discounts.Any())
            {
                dbContext.Discounts.Add(discont1);
                dbContext.Discounts.Add(discont2);
                dbContext.Discounts.Add(discont3);
            }


            if (!dbContext.Stores.Any())
            {
                dbContext.Stores.Add(store1);
                dbContext.Stores.Add(store2);
            }

            dbContext.SaveChanges();
        }
    }
}
