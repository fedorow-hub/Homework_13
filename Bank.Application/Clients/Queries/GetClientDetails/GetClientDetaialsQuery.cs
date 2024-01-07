using MediatR;

namespace Bank.Application.Clients.Queries.GetClientDetails;

public record GetClientDetaialsQuery : IRequest<ClientDetailsVM>
{
    public Guid ClientId { get; init; }
}
