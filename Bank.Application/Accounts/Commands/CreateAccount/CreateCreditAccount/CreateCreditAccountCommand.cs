using Bank.Domain.Client;
using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount.CreateCreditAccount;

public record CreateCreditAccountCommand : IRequest
{
    public Client Client { get; init; }

    public string Currency { get; init; }

    public decimal Amount { get; init; }

    public byte TermOfMonth { get; init; }
}
