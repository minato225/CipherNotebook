using Client.WPF.Services.CipherService;
using Client.WPF.State.Authenticators;
using Client.WPF.State.Navigators;
using Client.WPF.ViewModels;
using Client.WPF.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Client.WPF.HostBuilders;

public static class AddViewModelsHostBuilderExtensions
{
    public static IHostBuilder AddViewModels(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddTransient(CreateHomeViewModel);
            services.AddTransient<MainViewModel>();

            services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
            services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));
            services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));

            services.AddSingleton<IViewModelFactory, ViewModelFactory>();

            services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
            services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
            services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();
        });

        return host;
    }

    private static HomeViewModel CreateHomeViewModel(IServiceProvider services) =>
        new(services.GetRequiredService<ICypherFileTranspher>());

    private static LoginViewModel CreateLoginViewModel(IServiceProvider services) =>
        new(
            services.GetRequiredService<IAuthenticator>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>());

    private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
    => new(
        services.GetRequiredService<IAuthenticator>(),
        services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
        services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
}
