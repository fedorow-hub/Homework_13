using Bank.Application.Common.Exeptions;
using Bank.Application.Interfaces;
using Bank.Domain.Client;
using MediatR;

namespace Bank.Application.Clients.Commands.DeleteClient;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
{
    private readonly IClientRepository _clientsDbContext;

    public DeleteClientCommandHandler(IClientRepository clientsDbContext)
    {
        _clientsDbContext = clientsDbContext;
    }
    //public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    //{
    //    var targetClient = await _clientsDbContext.GetClient(request.Id, cancellationToken);
    //    if (targetClient == null)
    //    {
    //        throw new NotFoundException(nameof(Client), request.Id);
    //    }
    //    await _clientsDbContext.DeleteClient(request.Id, cancellationToken);
    //    return Unit.Value;
    //}

    public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var targetClient = await _clientsDbContext.GetClient(request.Id, cancellationToken);
        if (targetClient == null)
        {
            throw new NotFoundException(nameof(Client), request.Id);
        }
        await _clientsDbContext.DeleteClient(request.Id, cancellationToken);
        return;
    }
}
