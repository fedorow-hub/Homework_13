using Bank.Application.Interfaces;
using MediatR;

namespace Bank.Application.Clients.Queries.GetClientList;

public class GetClientListQueryHandler : IRequestHandler<GetClientListQuery, ClientListVM>
{
    private readonly IClientRepository _clientsDbContext;

    public GetClientListQueryHandler(IClientRepository clientsDbContext)
    {
        _clientsDbContext = clientsDbContext;
    }

    public async Task<ClientListVM> Handle(GetClientListQuery request, CancellationToken cancellationToken)
    {
        var clientsQuery = await _clientsDbContext.GetListClient(cancellationToken);

        return clientsQuery;
    }
}
