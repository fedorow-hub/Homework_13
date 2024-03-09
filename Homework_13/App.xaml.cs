using Bank.Application;
using Bank.Application.Common.Mapping;
using Bank.Application.Interfaces;
using Bank.DAL;
using Bank.DAL.ExchangeRateService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using Homework_13.ViewModels.Base;
using Serilog;
using MediatR.NotificationPublishers;
using Homework_13.Services;
using System.Data.Common;
using Bank.DAL.DataProviderAdoNet;

#if PC
using System.Data.OleDb;
#endif
using Microsoft.Data.SqlClient;

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
        
        services.AddViewModels();

        var (provider, connectionString, confBuilder) = GetProviderFromConfiguration();

        //services.AddBankDal(connectionString!);

        DbProviderFactory factory = GetDbProviderFactory(provider);
                
        services.AddSingleton<DbProviderFactory>(factory);
        services.AddSingleton<ConnectionString>(new ConnectionString { String = connectionString });
        services.AddSingleton<IDataProvider, DataProviderAdoNet>();

        var urlExchangeService = confBuilder.GetConnectionString("UrlExchangeService");

        services.AddSingleton<string>(urlExchangeService);

        services.AddSingleton<IExchangeRateService, ExchangeRateService>();

        services.AddSingleton<ICurrentWorkerService, CurrentWorkerService>();

        services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
        });
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        IsDesignMode = false;
        var host = Host;
        base.OnStartup(e);
        await host.StartAsync().ConfigureAwait(false);
        try
        {
            DbInitializer.Initialize(host.Services.GetRequiredService<ApplicationDbContext>());
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Не удаалось инициализировать EF Core");
        }
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

    private static string? GetSourceCodePath([CallerFilePath] string? path = null) => path;

    static (DataProviderEnum Provider, string ConnectionString, IConfigurationRoot ConfBuilder) GetProviderFromConfiguration()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        var providerName = config["ProviderName"];

        if (Enum.TryParse<DataProviderEnum>(providerName, out DataProviderEnum provider))
        {
            return (provider, config[$"{providerName}:ConnectionString"], config);
        };
       
        throw new Exception("Invalid data provider value supplied.");
    }

    enum DataProviderEnum
    {
        SQLite,
        SqlServer,        
#if PC
    OleDb,
#endif
        None
    }

    static DbProviderFactory GetDbProviderFactory(DataProviderEnum provider)
 => provider switch
 {
     DataProviderEnum.SqlServer => SqlClientFactory.Instance,
     DataProviderEnum.SQLite => SqlClientFactory.Instance,
#if PC
     DataProviderEnum.OleDb => OleDbFactory.Instance,
#endif
     _ => null
 };
}
