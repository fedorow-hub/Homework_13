using Bank.Application.Interfaces;
using MediatR;

namespace Bank.Application.Clients.Queries.GetClientList;

public class GetClientListQueryHandler : IRequestHandler<GetClientListQuery, ClientListVm>
{
    private readonly IDataProvider _dataProvider;

    public GetClientListQueryHandler(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task<ClientListVm> Handle(GetClientListQuery request, CancellationToken cancellationToken)
    {
        var clientsQuery = _dataProvider.GetClientList();

        return new ClientListVm { Clients = clientsQuery.Clients };
    }
}
