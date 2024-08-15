using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

public static class Program
{
    public static IServiceCollection AddCore(this IServiceCollection _serviceCollection)
    {
        _serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        return _serviceCollection;
    }
    
}