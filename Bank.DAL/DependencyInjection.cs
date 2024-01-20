using Bank.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bank.DAL;

public static class DependencyInjection
{

    public static IServiceCollection AddBankDAL(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString);
            options.LogTo(Console.WriteLine, LogLevel.Error);
        });
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

        return services;
    }
}
