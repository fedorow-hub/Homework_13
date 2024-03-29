﻿using Bank.Application;
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
        
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        var connectionString = builder.Build().GetConnectionString("DbConnection");

        services.AddBankDal(connectionString!);

        var urlExchangeServise = builder.Build().GetConnectionString("UrlExchangeService");

        services.AddSingleton<string>(urlExchangeServise);

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
}
