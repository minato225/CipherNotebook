﻿using CipherNoteBook.Server.Service;
using Microsoft.Extensions.Options;

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CipherNoteBook.Server.Helper;

public static class ServiceCollectionExtensions
{
    [Obsolete]
    public static void ConfigureWritable<T>(
        this IServiceCollection services, 
        IConfigurationSection section, 
        string file = "appsettings.json") where T : class, new()
    {
        services.Configure<T>(section);
        services.AddTransient<IWritableOptions<T>>(provider =>
        {
            var configuration = (IConfigurationRoot)provider.GetService<IConfiguration>();
            var environment = provider.GetService<IHostingEnvironment>();
            var options = provider.GetService<IOptionsMonitor<T>>();
            return new WritableOptions<T>(environment, options, configuration, section.Key, file);
        });
    }
}