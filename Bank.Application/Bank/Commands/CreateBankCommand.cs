using MediatR;

namespace Bank.Application.Bank.Commands;

public record CreateBankCommand : IRequest
{
    public string Name { get; init; }

    public decimal Capital { get; init; }
}
