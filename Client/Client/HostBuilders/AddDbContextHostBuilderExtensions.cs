using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Client.EntityFramework;
using System;

namespace Client.HostBuilders;

public static class AddDbContextHostBuilderExtensions
{
    public static IHostBuilder AddDbContext(this IHostBuilder host) => host
        .ConfigureServices((context, services) =>
        {
            var connectionString = context.Configuration.GetConnectionString("sqlite");
            Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlite(connectionString);

            services.AddDbContext<SimpleTraderDbContext>(configureDbContext);
            services.AddSingleton<DbContextFactory>(new DbContextFactory(configureDbContext));
        });
}
