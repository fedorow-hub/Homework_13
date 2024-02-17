using MediatR;

namespace Bank.Application.Clients.Queries.GetClientList;

public record GetClientListQuery : IRequest<ClientListVm>;
