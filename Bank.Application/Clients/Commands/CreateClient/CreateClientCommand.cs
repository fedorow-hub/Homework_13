using MediatR;

namespace Bank.Application.Clients.Commands.CreateClient
{
    public record CreateClientCommand : IRequest
    {
        public Guid Id { get; init; }
        public string Firstname { get; init; } = null!;

        public string Lastname { get; init; } = null!;

        public string Patronymic { get; init; } = null!;

        public string PhoneNumber { get; init; } = null!;

        public string PassportSeries { get; init; } = null!;

        public string PassportNumber { get; init; } = null!;

        public string TotalIncomePerMounth { get; init; } = null!;
    }
}
