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
                .HasMany(e => e.Stores)
                .WithMany(s => s.Employees);


            modelBuilder.Entity<Product>()
              .HasOne(p => p.Store)
              .WithMany(s => s.Products);


            modelBuilder.Entity<Product>()
              .HasMany(p => p.BalanceProducts)
              .WithOne(bp => bp.Product);


            modelBuilder.Entity<Coming>()
                .HasMany(c => c.BalanceProducts)
                .WithOne(bp => bp.Coming);


            modelBuilder.Entity<Coming>()
                .HasOne(c => c.Provider)
                .WithMany(p => p.Comings);


            modelBuilder.Entity<Coming>()
                 .HasOne(c => c.Store)
                 .WithMany(s => s.Comings);
            //            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ReservationProduct>()
              .HasOne(rp => rp.Order)
              .WithMany(o => o.ReservationProducts);


            modelBuilder.Entity<Order>()
              .HasOne(o => o.Store)
              .WithMany(s => s.Orders);


            modelBuilder.Entity<Provider>()
               .HasOne(p => p.Employee)
               .WithMany(e => e.Providers);


            //  уникальность наименования
            modelBuilder.Entity<Discount>()
                 .HasIndex(d => d.Name)
                 .IsUnique()
                .HasFilter(null);




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
                Address = "адрес",
                Phone = "0967636792"
            };

            var employees2 = new Employee
            {
                Name = "Anna",
                Email = "litv@gmail.com",
                Role = Role.Manager,
                SurName = "Litvintceva",
                Address = "адрес",               
                Phone = "0666666989"
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
                Name = "Black Fride_1",
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
