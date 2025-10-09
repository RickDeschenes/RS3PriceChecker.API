using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RS3PriceChecker.Models;
using System;
using System.IO;
using System.Reflection;

namespace RS3PriceChecker.Database
{
    public class RS3PriceCheckerDBContext : DbContext
    {
        public RS3PriceCheckerDBContext(DbContextOptions<RS3PriceCheckerDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<MostRecentPrices>(
                    eb =>
                    {
                        eb.HasNoKey();
                        eb.ToView("MostRecentPrice");
                    });
        }

        public virtual DbSet<Items> Item { get; set; }
        public virtual DbSet<Categories> Category { get; set; }
        public virtual DbSet<Names> Name { get; set; }
        public virtual DbSet<Icons> Icon { get; set; }
        public virtual DbSet<Prices> Price { get; set; }
        public virtual DbSet<MostRecentPrices> MostRecentPrice { get; set; }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RS3PriceCheckerDBContext>
    {
        public RS3PriceCheckerDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../RS3PriceChecker.API/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<RS3PriceCheckerDBContext>();
            var connectionString = configuration.GetConnectionString("RS3PriceCheckerAlias");
            builder.UseSqlServer(connectionString);
            return new RS3PriceCheckerDBContext(builder.Options);
        }
    }
}
