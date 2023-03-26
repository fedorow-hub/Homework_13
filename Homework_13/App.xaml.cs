using BankDAL.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Homework_13;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    
    public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
    {
        services.AddSingleton<DataAccess>();
    }
}
