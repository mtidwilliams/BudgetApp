using System.Reflection;
using Microsoft.Extensions.Configuration;

public static class Program
{
    public static IConfigurationBuilder AddCommonConfig(this IConfigurationBuilder _builder)
    {
        string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "C:\\"; //NOSONAR

        _builder.AddJsonFile(Path.Combine(filePath, "appsettings.json"));

        return _builder;
    }
    
}