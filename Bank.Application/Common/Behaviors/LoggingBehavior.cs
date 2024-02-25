using Serilog;
using MediatR;

namespace Bank.Application.Common.Behaviors
{
    internal class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest 
        : IRequest<TResponse>
    {
        
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            Log.Information("Request: {Name} {@request}", requestName, request);

            var response = await next();

            return response;
        }
    }
}
