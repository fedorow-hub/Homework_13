using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Bank.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //регистрация сервиса MediatR с помощью метода AddMediatR из Nuget пакета
            //MediatR.Extensions.Microsoft.DependencyInjection
            //services.AddMediatR(Assembly.GetExecutingAssembly());//в метод передается выполняемая сборка 
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
