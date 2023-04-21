using Bank.Application.Interfaces;
using MediatR;

namespace Bank.Application.Clients.Commands.DeleteClient;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
{
    private readonly IClientRepository _clientsDbContext;

    public DeleteClientCommandHandler(IClientRepository clientsDbContext)
    {
        _clientsDbContext = clientsDbContext;
    }
    public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        await _clientsDbContext.DeleteClient(request.Id, cancellationToken);
        return Unit.Value;
    }
}
