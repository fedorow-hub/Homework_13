using MediatR;

namespace Bank.Application.Accounts.Commands.CloseAccount;

public record CloseAccountCommand : IRequest<string>
{
    public Guid Id { get; init; }
}
