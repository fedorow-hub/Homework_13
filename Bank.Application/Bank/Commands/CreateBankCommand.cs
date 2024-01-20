using Bank.Domain.Bank;
using MediatR;

namespace Bank.Application.Bank.Commands;

public record CreateBankCommand : IRequest<SomeBank>
{
    public string Name { get; init; }

    public decimal Capital { get; init; }

    public DateTime DateOfCreation { get; init; }
}
