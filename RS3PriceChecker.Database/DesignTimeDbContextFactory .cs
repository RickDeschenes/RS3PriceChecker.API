using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RS3PriceChecker.Database;

namespace RS3PriceChecker.Services;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RS3PriceCheckerDBContext>
{
    public RS3PriceCheckerDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RS3PriceCheckerDBContext>();
        optionsBuilder.UseSqlServer("Data Source=dads-pc;Initial Catalog=RS3PriceChecker;Integrated Security=True;Encrypt=False;Connect Timeout=30;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        return new RS3PriceCheckerDBContext(optionsBuilder.Options);
    }
}

