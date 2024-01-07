using MediatR;

namespace Bank.Application.Clients.Commands.DeleteClient;

public record DeleteClientCommand : IRequest
{
    public Guid Id { get; init; }
}
