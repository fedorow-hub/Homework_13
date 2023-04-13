using Bank.Domain.Client;
using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount.CreateCreditAccount;

public record CreditAccountCreateDTO : IRequest
{
    public long ClientId { get; init; }

    public string Currency { get; init; }

    public DateTime TimeOfCreated { get; init; }

    public decimal Amount { get; init; }

    public string LoanInterest { get; init; }

    public DateTime AccountTerm { get; init; }

    public bool IsExistance { get; init; }
}