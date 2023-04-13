using MediatR;

namespace Bank.Application.Clients.Commands.DeleteClient;

public class DeleteClientCommand : IRequest
{
    public long Id { get; set; }
}
