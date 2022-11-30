using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using CipherNoteBook.Domain.Services.AuthService.interfaces;
using CipherNoteBook.Domain.Models;
using CipherNoteBook.OTP.State.Authenticators;
using CipherNoteBook.Domain.Services.AuthService;
using CipherNoteBook.OTP.State.Navigators;
using System;
using CipherNoteBook.Domain.Services.OtpService;

namespace CipherNoteBook.OTP.HostBuilders;

public static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host) => host
        .ConfigureServices(services => services
                .AddSingleton<IPasswordHasher<Account>, PasswordHasher<Account>>()
                .AddSingleton<IAuthenticatorService, AuthenticatorService>()
                .AddSingleton<IAuthenticator, Authenticator>()
                .AddSingleton<INavigator, Navigator>()
                .AddSingleton<IOtpService, OtpService>()
                .AddHttpClient<IAuthenticatorService, AuthenticatorService>(c => c
                    .BaseAddress = new Uri(@"https://localhost:7160")));
}
