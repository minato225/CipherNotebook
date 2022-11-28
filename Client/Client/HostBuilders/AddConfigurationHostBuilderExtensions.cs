using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace Client.WPF.HostBuilders;

public static class AddConfigurationHostBuilderExtensions
{
    public static IHostBuilder AddConfiguration(this IHostBuilder host) =>
        host.ConfigureAppConfiguration(c =>
        {
            c.AddJsonFile("appsettings.json");
            c.AddEnvironmentVariables();
        });
}