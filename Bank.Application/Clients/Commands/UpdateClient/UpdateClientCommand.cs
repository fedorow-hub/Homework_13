using MediatR;

namespace Bank.Application.Clients.Commands.UpdateClient;

public record UpdateClientCommand : IRequest
{
    public long Id { get; init; }

    public string Firstname { get; init; }

    public string Lastname { get; init; }

    public string Patronymic { get; init; }

    public string PhoneNumber { get; init; }

    public string PassportSerie { get; init; }

    public string PassportNumber { get; init; }

    public decimal TotalIncomePerMounth { get; init; }
}
