using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using Client.WPF.Services.AuthService;
using Client.WPF.Services.CipherService;
using Client.WPF.Services.AuthService.interfaces;
using Client.DataBase.Models;
using Microsoft.AspNetCore.Identity;

namespace Client.WPF.HostBuilders;

public static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host) => host
        .ConfigureServices(services =>
        {
            services.AddSingleton<IPasswordHasher<Account>, PasswordHasher<Account>>();

            services.AddHttpClient<ICypherFileTranspher, CypherFileTranspher>(c =>
            {
                c.BaseAddress = new Uri(@"https://localhost:44341/CipherText");
            });

            services.AddSingleton<IAuthenticatorService, AuthenticatorService>();
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<IAccountService, AccountDataService>();
        });
}
