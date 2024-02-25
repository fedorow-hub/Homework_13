using MediatR;

namespace Bank.Application.Clients.Commands.UpdateClient;

public class UpdateClientEvent : INotification
{
    public Guid Id { get; init; }
}
