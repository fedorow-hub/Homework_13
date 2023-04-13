using MediatR;

namespace Bank.Application.Clients.Commands.UpdateClient;

public class UpdateClientCommand : IRequest
{
    public long Id { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Patronymic { get; set; }

    public string PhoneNumber { get; set; }

    public string PassportSerie { get; set; }

    public string PassportNumber { get; set; }

    public decimal TotalIncomePerMounth { get; set; }
}
