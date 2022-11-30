using CipherNoteBook.Domain.Services.OtpService;
using CipherNoteBook.OTP.State.Authenticators;
using CipherNoteBook.OTP.State.Navigators;
using CipherNoteBook.OTP.ViewModels;
using CipherNoteBook.OTP.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CipherNoteBook.OTP.HostBuilders;

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

            services.AddSingleton<IViewModelFactory, ViewModelFactory>();

            services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
            services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
        });

        return host;
    }

    private static HomeViewModel CreateHomeViewModel(IServiceProvider services) =>
        new(
            services.GetRequiredService<IOtpService>(),
            services.GetRequiredService<IAuthenticator>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());

    private static LoginViewModel CreateLoginViewModel(IServiceProvider services) =>
        new(
            services.GetRequiredService<IAuthenticator>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>());
}
