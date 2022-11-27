using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Client.ViewModels;

namespace Client.HostBuilders;

public static class AddViewsHostBuilderExtensions
{
    public static IHostBuilder AddViews(this IHostBuilder host) => host
        .ConfigureServices(services => 
            services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainViewModel>())));
}
