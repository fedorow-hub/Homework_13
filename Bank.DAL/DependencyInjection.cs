using Bank.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Bank.DAL
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddBankDAL(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ClientDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            services.AddScoped<IClientDbContext>(provider => provider.GetService<ClientDbContext>());
            
            return services;
        }
    }
}
