using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Client.DataBase;

namespace Client.WPF.HostBuilders;

public static class AddDbContextHostBuilderExtensions
{
    public static IHostBuilder AddDbContext(this IHostBuilder host) => host
        .ConfigureServices((context, services) =>
        {
            var connectionString = context.Configuration.GetConnectionString("sqlite");
            void configureDbContext(DbContextOptionsBuilder o) => o.UseSqlite(connectionString);

            services.AddDbContext<ClientDataContext>(configureDbContext);
            services.AddSingleton(new ClientDbContextFactory(configureDbContext));
        });
}
