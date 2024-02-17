using Bank.Application.Common.Exeptions;
using Bank.Application.Interfaces;
using Bank.Domain.Client;
using MediatR;

namespace Bank.Application.Clients.Queries.GetClientDetails;

public class GetClientDetailsQueryHandler : IRequestHandler<GetClientDetaialsQuery, ClientDetailsVm>
{
    public GetClientDetailsQueryHandler()
    {
    }

    public async Task<ClientDetailsVm> Handle(GetClientDetaialsQuery request, CancellationToken cancellationToken)
    {
        
        var client = new ClientDetailsVm();
        if (client == null)
        {
            throw new NotFoundException(nameof(Client), request.ClientId);
        }

        return client;
    }
}
