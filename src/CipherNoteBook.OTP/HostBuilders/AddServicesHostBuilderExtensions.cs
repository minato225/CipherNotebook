using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using CipherNoteBook.Domain.Services.AuthService.interfaces;
using CipherNoteBook.Domain.Models;
using CipherNoteBook.DataBase.Services;
using CipherNoteBook.OTP.State.Authenticators;
using CipherNoteBook.Domain.Services.AuthService;
using CipherNoteBook.OTP.State.Navigators;

namespace CipherNoteBook.OTP.HostBuilders;

public static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host) => host
        .ConfigureServices(services => services
            .AddSingleton<IPasswordHasher<Account>, PasswordHasher<Account>>()
            .AddSingleton<IAuthenticatorService, AuthenticatorService>()
            .AddSingleton<IDataService<Account>, AccountDataService>()
            .AddSingleton<IAccountService, AccountDataService>()
            .AddSingleton<IAuthenticator, Authenticator>()
            .AddSingleton<INavigator, Navigator>());
}
