using BudgetApp.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

public static class Program
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection _serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        _serviceCollection.AddDbContext<BudgetAppDbContext>(options =>
           options.UseSqlServer(connectionString));

        return _serviceCollection;
    }
    
}