using Bank.Application.Accounts.Commands.CreateAccount.CreateCreditAccount;
using Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;
using Bank.Domain.Account;

namespace Bank.Application.Interfaces;

public interface IAccountRepository
{
    Task CreatePlainAccount(PlainAccountCreateDTO plainAccount, CancellationToken cancellationToken);
    Task CreateDepositAccount(DepositAccountCreateDTO plainAccount, CancellationToken cancellationToken);
    Task CreateCreditAccount(CreditAccountCreateDTO plainAccount, CancellationToken cancellationToken);    
    Task<Account> GetConcreteAccount(long id, TypeOfAccount typeOfAccount, CancellationToken cancellationToken);
    Task SaveChangesAccount(Account account, CancellationToken cancellationToken);
    Task TransactionBetweenAccounts(Account accountFrom, Account accountDestination);    
}
