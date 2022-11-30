using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Identity;
using CipherNoteBook.Domain.Services.AuthService.interfaces;
using CipherNoteBook.Domain.Services.CipherService;
using CipherNoteBook.Domain.Models;
using CipherNoteBook.DataBase.Services;
using CipherNoteBook.WPF.State.Navigators;
using CipherNoteBook.Domain.Services.AuthService;
using CipherNoteBook.WPF.State.Authenticators;

namespace CipherNoteBook.WPF.HostBuilders;

public static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host) => host
        .ConfigureServices(services =>
        {
            services.AddHttpClient<ICypherFileTranspher, CypherFileTranspher>(c =>
            {
                c.BaseAddress = new Uri(@"https://localhost:7160");
            });

            services.AddHttpClient<IAuthenticatorService, AuthenticatorService>(c =>
            {
                c.BaseAddress = new Uri(@"https://localhost:7160");
            });

            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IAuthenticator, Authenticator>();
        });
}
