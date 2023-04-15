using Bank.Application.Clients.Queries.GetClientList;
using Bank.Application.Interfaces;
using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Clients.Queries.GetClientDetails;

public class GetClientDetailsQueryHandler : IRequestHandler<GetClientDetaialsQuery, ClientDetailsVM>
{
    private readonly IClientRepository _clientRepository;
    private readonly IAccountRepository _accountRepository;
    public GetClientDetailsQueryHandler(IAccountRepository accountRepository, IClientRepository clientRepository)
    {
        _accountRepository = accountRepository;
        _clientRepository = clientRepository;
    }

    public async Task<ClientDetailsVM> Handle(GetClientDetaialsQuery request, CancellationToken cancellationToken)
    {
        List<Account> accounts = (await _accountRepository.GetAccountsConcreteClient(request.ClientId, cancellationToken)).Accounts;
        ClientLookUpDTO client = await _clientRepository.GetClient(request.ClientId, cancellationToken);
        var clientDetailsVM = new ClientDetailsVM
        {
            Firstname = client.Firstname,
            Lastname = client.Lastname,
            Patronymic = client.Patronymic,
            PhoneNumber = client.PhoneNumber,
            PassportSerie = client.PassportSerie,
            PassportNumber = client.PassportNumber,
            Accounts = accounts,
        };

        return clientDetailsVM;
    }
}
