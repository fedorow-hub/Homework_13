using Bank.Application.Accounts.Commands.CreateAccount.CreateCreditAccount;
using Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;

namespace Bank.Application.Interfaces;

public interface IAccountRepository
{
    Task CreatePlainAccount(PlainAccountCreateDTO plainAccount, CancellationToken cancellationToken);
    Task CreateDepositAccount(DepositAccountCreateDTO plainAccount, CancellationToken cancellationToken);
    Task CreateCreditAccount(CreditAccountCreateDTO plainAccount, CancellationToken cancellationToken);
    //Task UpdateClient(ClientUpdateDTO client, CancellationToken cancellationToken);
    //Task DeleteClient(long id, CancellationToken cancellationToken);
    //Task<ClientListVM> GetListClient();
}
