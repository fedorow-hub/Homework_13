
using MediatR;

namespace Bank.Application.Clients.Commands.CreateClient;

public class CreateClientEvent : INotification
{
    public Guid Id { get; init; }

    public string Firstname { get; init; } 

    public string Lastname { get; init; } 

    public string Patronymic { get; init; } 
}

