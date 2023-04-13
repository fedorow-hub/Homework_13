using Bank.Application.Common.Mapping;
using Bank.Domain.Account;

namespace Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;

public record DepositAccountCreateDTO
{
    public long ClientId { get; init; }

    public string Currency { get; init; }

    public DateTime TimeOfCreated { get; init; }

    public decimal Amount { get; init; }
            
    public string InterestRate { get; init; } 
        
    public DateTime AccountTerm { get; init; }

    public bool IsExistance { get; init; }
}