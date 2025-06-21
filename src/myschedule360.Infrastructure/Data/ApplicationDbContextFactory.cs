using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace myschedule360.Infrastructure.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        // Use a default connection string for migrations
        optionsBuilder.UseNpgsql("Host=localhost;Database=myschedule360;Username=postgres;Password=postgres");
        
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}