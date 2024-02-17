using MediatR;

namespace Bank.Application.Clients.Queries.GetClientDetails;

public record GetClientDetaialsQuery : IRequest<ClientDetailsVm>
{
    public Guid ClientId { get; init; }
}
