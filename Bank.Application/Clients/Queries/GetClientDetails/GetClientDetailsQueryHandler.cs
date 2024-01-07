using Bank.Application.Common.Exeptions;
using Bank.Application.Interfaces;
using Bank.Domain.Client;
using MediatR;

namespace Bank.Application.Clients.Queries.GetClientDetails;

public class GetClientDetailsQueryHandler : IRequestHandler<GetClientDetaialsQuery, ClientDetailsVM>
{
    private readonly IClientRepository _clientRepository;
    //private readonly IAccountRepository _accountRepository;
    public GetClientDetailsQueryHandler(IClientRepository clientRepository)
    {
        //_accountRepository = accountRepository;
        _clientRepository = clientRepository;
    }

    public async Task<ClientDetailsVM> Handle(GetClientDetaialsQuery request, CancellationToken cancellationToken)
    {
        //List<Account> accounts = (await _accountRepository.GetAccountsConcreteClient(request.ClientId, cancellationToken)).Accounts;
        ClientDetailsVM client = await _clientRepository.GetClient(request.ClientId, cancellationToken);
        if (client == null)
        {
            throw new NotFoundException(nameof(Client), request.ClientId);
        }

        return client;
    }
}
