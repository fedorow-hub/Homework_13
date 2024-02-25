
using Bank.Application.Interfaces;
using MediatR;
using Serilog;

namespace Bank.Application.Clients.Commands.UpdateClient;

public class UpdateClientEventHandler : INotificationHandler<UpdateClientEvent>
{
    private readonly ICurrentWorkerService _workerService;
    public UpdateClientEventHandler(ICurrentWorkerService workerService)
    {
        _workerService = workerService;
    }

    public Task Handle(UpdateClientEvent notification, CancellationToken cancellationToken)
    {
        Log.Information($"{_workerService.Worker} изменил данные клиента {notification.Id} ");
        return Task.CompletedTask;
    }
}

