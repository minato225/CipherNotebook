using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Client.HostBuilders;

public static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host) => host
        .ConfigureServices(services =>
        {
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<IAccountService, AccountDataService>();
        });
}
