using Bank.Application.Interfaces;
using MediatR;

namespace Bank.Application.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
{
    private readonly IClientRepository _clientsDbContext;

    public UpdateClientCommandHandler(IClientRepository clientsDbContext)
    {
        _clientsDbContext = clientsDbContext;
    }

    public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new ClientUpdateDTO
        {
            Id = request.Id,
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            Patronymic = request.Patronymic,
            PhoneNumber = request.PhoneNumber,
            PassportSerie = request.PassportSerie,
            PassportNumber = request.PassportNumber,
            TotalIncomePerMounth = request.TotalIncomePerMounth
        };

        await _clientsDbContext.UpdateClient(client, cancellationToken);
    }
}
