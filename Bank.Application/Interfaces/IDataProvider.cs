using Bank.Application.Accounts;
using Bank.Application.Clients.Queries.GetClientList;
using Bank.Domain.Account;
using Bank.Domain.Bank;
using Bank.Domain.Client;

namespace Bank.Application.Interfaces;

public interface IDataProvider
{
    bool CreateBank(SomeBank bank);
    SomeBank GetBank();
    bool UpdateBankCapital(SomeBank bank);

    bool CreateClient(Client client);
    bool UpdateClient(Client client);
    bool DeleteClient(Guid clientId);
    ClientListVm GetClientList();
    Client GetClient(Guid id);

    bool CreateAccount(Account account);
    bool CloseAccount(Account account);
    bool ChangeAmountOfAccount(Account account);
    Account GetAccount(Guid id);
    AccountListVm GetAccountList(Guid clientId);
}
