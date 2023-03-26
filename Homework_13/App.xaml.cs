using BankDAL.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace Homework_13;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static IHost _host;
    public static IHost Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
    public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
    {
        services.AddSingleton<DataAccess>();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        var host = Host;
        base.OnStartup(e);
        await host.StartAsync().ConfigureAwait(false);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        var host = Host;
        base.OnExit(e);
        await host.StopAsync().ConfigureAwait(false);
        host.Dispose();
        _host = null;
    }
}
