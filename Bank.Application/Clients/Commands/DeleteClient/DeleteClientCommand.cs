using MediatR;

namespace Bank.Application.Clients.Commands.DeleteClient;

public record DeleteClientCommand : IRequest
{
    public long Id { get; init; }
}
