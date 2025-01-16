using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BudgetApp.Infrastructure.Data;
public class BudgetAppDbContextFactory : IDesignTimeDbContextFactory<BudgetAppDbContext>
{
    public BudgetAppDbContext CreateDbContext(string[] args)
    {
        string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "C:\\";
         
        var configuration = new ConfigurationBuilder()
            // .SetBasePath(Directory.GetCurrentDirectory())
            // .AddJsonFile("appsettings.json")
            .AddJsonFile(Path.Combine(filePath, "appsettings.json"), optional: true, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<BudgetAppDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);

        return new BudgetAppDbContext(optionsBuilder.Options);
    }
}
