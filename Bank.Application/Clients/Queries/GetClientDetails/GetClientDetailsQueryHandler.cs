using Bank.Application.Common.Exeptions;
using Bank.Application.Interfaces;
using Bank.Domain.Client;
using MediatR;

namespace Bank.Application.Clients.Queries.GetClientDetails;

public class GetClientDetailsQueryHandler : IRequestHandler<GetClientDetaialsQuery, ClientDetailsVM>
{
    public GetClientDetailsQueryHandler()
    {
    }

    public async Task<ClientDetailsVM> Handle(GetClientDetaialsQuery request, CancellationToken cancellationToken)
    {
        
        ClientDetailsVM client = new ClientDetailsVM();
        if (client == null)
        {
            throw new NotFoundException(nameof(Client), request.ClientId);
        }

        return client;
    }
}
