using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR.NotificationPublishers;

namespace Bank.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.NotificationPublisher = new ForeachAwaitPublisher();
        });
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>)); 
        
        return services;
    }
}
