using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bank.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //регистрация сервиса MediatR с помощью метода AddMediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
