using MediatR;

namespace Bank.Application.Accounts.Queries;

public record GetAccountsQuery : IRequest<AccountListVm>
{
    public Guid Id { get; init; }
}

