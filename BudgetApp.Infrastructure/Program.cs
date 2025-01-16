using BudgetApp.Core.Common.Interfaces;
using BudgetApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class Program
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection _serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        _serviceCollection.AddDbContext<BudgetAppDbContext>(options =>
           options.UseSqlServer(connectionString));

        _serviceCollection.AddScoped<IDatabaseService>(provider => provider.GetService<BudgetAppDbContext>());

        return _serviceCollection;
    }
}