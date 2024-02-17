using MediatR;

namespace Bank.Application.Clients.Commands.UpdateClient;

public record UpdateClientCommand : IRequest
{
    public Guid Id { get; init; }

    public string Firstname { get; init; } = null!;

    public string Lastname { get; init; } = null!;

    public string Patronymic { get; init; } = null!;

    public string PhoneNumber { get; init; } = null!;

    public string PassportSeries { get; init; } = null!;
            
    public string PassportNumber { get; init; } = null!;

    public decimal TotalIncomePerMounth { get; init; }
}
