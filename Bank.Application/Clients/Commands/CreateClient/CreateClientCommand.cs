using MediatR;

namespace Bank.Application.Clients.Commands.CreateClient
{
    /// <summary>
    /// данный класс содержит только информацию о том, что необходимо для создания клиента
    /// </summary>
    public class CreateClientCommand : IRequest
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportSerie { get; set; }

        public string PassportNumber { get; set; }

        public decimal TotalIncomePerMounth { get; set; }
    }
}
