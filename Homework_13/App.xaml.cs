using Bank.Application;
using Bank.Application.Common.Mapping;
using Bank.Application.Interfaces;
using Bank.DAL;
using Bank.DAL.ExchangeRateService;
using Homework_13.Models.Bank;
using Homework_13.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Homework_13;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    
    public static bool IsDesignMode { get; private set; } = true;

    private static IHost _host;
    public static IHost Host =>
        _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
    
    public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
    {
        //сюда добавляем необходимые сервисы
        services.AddApplication();
        
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<LoginWindowViewModel>();

        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        string connectionString = builder.Build().GetConnectionString("DbConnection");

        services.AddBankDAL(connectionString);

        //services.AddSingleton<IClientRepository, ClientRepository>();

        string urlExchangeServise = builder.Build().GetConnectionString("UrlExchangeService");

        services.AddSingleton<IExchangeRateService>(new ExchangeRateService(urlExchangeServise));

        services.AddSingleton<BankRepository>();

        services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new AssemblyMappingProfile(typeof(IClientDbContext).Assembly));
        });

        //services.AddSingleton<ClientDAL>();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        IsDesignMode = false;
        var host = Host;

        using (var scope = host.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            try
            {
                var context = serviceProvider.GetRequiredService<ClientDbContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception exeption)
            {
                
            }
        }

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

    public static string CurrentDirectory => IsDesignMode
        ? Path.GetDirectoryName(GetSourceCodePath())
        : Environment.CurrentDirectory;

    private static string GetSourceCodePath([CallerFilePath] string Path = null) => Path;
}
