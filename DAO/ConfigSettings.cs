using Microsoft.Extensions.Configuration;
using System.IO;

namespace fullstackCsharp.DAO;
public static class ConfigSettings
{
    public static string connString { get; }
    static ConfigSettings()
    {
        var configurationBuilder = new ConfigurationBuilder();
        string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        configurationBuilder.AddJsonFile(path, false);
        connString = configurationBuilder.Build().GetSection("ConnectionStrings:AzureDB").Value;
    }
}