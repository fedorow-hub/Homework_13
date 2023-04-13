using MediatR;

namespace Bank.Application.Clients.Commands.CreateClient
{
    /// <summary>
    /// данный класс содержит только информацию о том, что необходимо для создания клиента
    /// </summary>
    public record CreateClientCommand : IRequest
    {
        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public string Patronymic { get; init; }

        public string PhoneNumber { get; init; }

        public string PassportSerie { get; init; }

        public string PassportNumber { get; init; }

        public decimal TotalIncomePerMounth { get; init; }
    }
}
