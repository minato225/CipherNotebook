using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CipherNoteBook.OTP.ViewModels;
using CipherNoteBook.OTP;

namespace CipherNoteBook.OTP.HostBuilders;

public static class AddViewsHostBuilderExtensions
{
    public static IHostBuilder AddViews(this IHostBuilder host) => host
        .ConfigureServices(services =>
            services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainViewModel>())));
}
