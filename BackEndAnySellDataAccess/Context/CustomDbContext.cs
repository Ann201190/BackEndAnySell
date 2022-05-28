using BackEndAnySellDataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEndAnySellAccessDataAccess.Context
{
    public class CustomDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ReservationProduct> ReservationProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Coming> Comings { get; set; }
        public DbSet<BalanceProduct> BalanceProducts { get; set; }
        public DbSet<Provider> Providers { get; set; }
        



        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
