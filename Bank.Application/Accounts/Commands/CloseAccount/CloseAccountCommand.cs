using MediatR;

namespace Bank.Application.Accounts.Commands.CloseAccount;

public record CloseAccountCommand : IRequest
{
    public Guid Id { get; init; }
}
