using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Serilog;
using Serilog.Events;

namespace Homework_13;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            .WriteTo.File("BankLogs\\BankAppLog-.txt", rollingInterval:
                RollingInterval.Day)
            .CreateLogger();
        var app = new App();
        app.InitializeComponent();
        app.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .UseContentRoot(App.CurrentDirectory)
            .ConfigureAppConfiguration((_, cfg) => cfg
                .SetBasePath(App.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true))
            .ConfigureServices(App.ConfigureServices);
}
