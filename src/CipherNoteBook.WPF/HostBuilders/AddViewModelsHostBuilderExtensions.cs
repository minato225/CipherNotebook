using CipherNoteBook.Domain.Services.CipherService;
using CipherNoteBook.WPF.State.Authenticators;
using CipherNoteBook.WPF.State.Navigators;
using CipherNoteBook.WPF.ViewModels;
using CipherNoteBook.WPF.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CipherNoteBook.WPF.HostBuilders;

public static class AddViewModelsHostBuilderExtensions
{
    public static IHostBuilder AddViewModels(this IHostBuilder host) => host
        .ConfigureServices(services => services
            .AddTransient(CreateHomeViewModel)
            .AddTransient<MainViewModel>()
            .AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>())
            .AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services))
            .AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services))
            .AddSingleton<IViewModelFactory, ViewModelFactory>()
            .AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>()
            .AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>()
            .AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>());

    private static HomeViewModel CreateHomeViewModel(IServiceProvider services) =>
        new(
            services.GetRequiredService<ICypherFileTranspher>(),
            services.GetRequiredService<IAuthenticator>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());

    private static LoginViewModel CreateLoginViewModel(IServiceProvider services) =>
        new(
            services.GetRequiredService<IAuthenticator>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>());

    private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services) =>
        new(
            services.GetRequiredService<IAuthenticator>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
}
