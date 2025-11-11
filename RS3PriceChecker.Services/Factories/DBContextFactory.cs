using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RS3PriceChecker.Database;

namespace RS3PriceChecker.Services.Factories;
public interface IDbContextFactory<RS3DbContext> where RS3DbContext : DbContext
{
    RS3DbContext CreateDbContext();
}

// usage dependency injection example:
// services.AddDbContext<RS3DbContext>(options =>
// options.UseSqlServer("your-connection-string"));

// usage repository async db call example:
// var dbContext = factory.CreateDbContextInstance();
// var result = await dbContext.YourDbSet.ToListAsync();

public class DBContextFactory(DbContextOptions<RS3DbContext> options) : IDbContextFactory<RS3DbContext>
{
    private readonly DbContextOptions<RS3DbContext> _options = options ?? throw new ArgumentNullException(nameof(options));

    public RS3DbContext CreateDbContext()
    {
        return new RS3DbContext(_options);
    }
}
