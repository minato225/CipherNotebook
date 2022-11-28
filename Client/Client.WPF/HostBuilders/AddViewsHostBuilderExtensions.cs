using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Client.WPF.ViewModels;

namespace Client.WPF.HostBuilders;

public static class AddViewsHostBuilderExtensions
{
    public static IHostBuilder AddViews(this IHostBuilder host)
    {
        return host
        .ConfigureServices(services =>
            services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainViewModel>())));
    }
}
