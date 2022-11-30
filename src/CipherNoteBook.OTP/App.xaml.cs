﻿using CipherNoteBook.DataBase;
using CipherNoteBook.OTP.HostBuilders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace CipherNoteBook.OTP;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    public App() => _host = CreateHostBuilder().Build();

    public static IHostBuilder CreateHostBuilder(string[]? args = null) =>
        Host
        .CreateDefaultBuilder(args)
        .AddServices()
        .AddViewModels()
        .AddViews();

    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();

        ShowMainWindow();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();

        base.OnExit(e);
    }

    private void ShowMainWindow() => _host.Services.GetRequiredService<MainWindow>().Show();
}
