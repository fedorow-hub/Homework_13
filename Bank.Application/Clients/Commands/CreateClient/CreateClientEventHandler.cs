using Bank.Application.Interfaces;
using MediatR;
using Serilog;

namespace Bank.Application.Clients.Commands.CreateClient;

public class CreateClientEventHandler : INotificationHandler<CreateClientEvent>
{
    private readonly ICurrentWorkerService _workerService;
    public CreateClientEventHandler(ICurrentWorkerService workerService)
    {
        _workerService = workerService;
    }
    public Task Handle(CreateClientEvent notification, CancellationToken cancellationToken)
    {
        Log.Information($"{_workerService.Worker} добавил клиента {notification.Id} {notification.Lastname} {notification.Firstname} {notification.Patronymic}");
        return Task.CompletedTask;
    }
}

