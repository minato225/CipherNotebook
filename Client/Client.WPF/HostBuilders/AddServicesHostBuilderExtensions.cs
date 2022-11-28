using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using Client.WPF.Services.AuthService;
using Client.WPF.Services.CipherService;
using Client.WPF.Services.AuthService.interfaces;
using Microsoft.AspNetCore.Identity;
using Client.WPF.State.Authenticators;
using Client.WPF.State.Navigators;
using Client.Domain.Models;

namespace Client.WPF.HostBuilders;

public static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host) => host
        .ConfigureServices(services =>
        {
            services.AddSingleton<IPasswordHasher<Account>, PasswordHasher<Account>>();

            services.AddHttpClient<ICypherFileTranspher, CypherFileTranspher>(c =>
            {
                c.BaseAddress = new Uri(@"https://localhost:44341");
            });

            services.AddSingleton<IAuthenticatorService, AuthenticatorService>();
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<IAccountService, AccountDataService>();

            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IAuthenticator, Authenticator>();
        });
}
