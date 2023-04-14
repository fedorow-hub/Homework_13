using Bank.Application.Accounts.Commands.CreateAccount.CreateCreditAccount;
using Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;
using Bank.Domain.Account;

namespace Bank.Application.Interfaces;

public interface IAccountRepository
{
    Task CreatePlainAccount(PlainAccountCreateDTO plainAccount, CancellationToken cancellationToken);
    Task CreateDepositAccount(DepositAccountCreateDTO plainAccount, CancellationToken cancellationToken);
    Task CreateCreditAccount(CreditAccountCreateDTO plainAccount, CancellationToken cancellationToken);
    Task DeleteAccount(long id, string TypeOfAccount, CancellationToken cancellationToken);
    Task<Account> GetConcreteAccount(long id, TypeOfAccount typeOfAccount, CancellationToken cancellationToken);
    Task TransactionBetweenAccounts(Account accountFrom, Account accountDestination);
    //Task UpdateClient(ClientUpdateDTO client, CancellationToken cancellationToken);
    //Task DeleteClient(long id, CancellationToken cancellationToken);
    //Task<ClientListVM> GetListClient();
}
