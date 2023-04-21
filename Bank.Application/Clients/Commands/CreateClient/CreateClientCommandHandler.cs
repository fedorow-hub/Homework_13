using Bank.Application.Interfaces;
using MediatR;

namespace Bank.Application.Clients.Commands.CreateClient;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand>
{
    private readonly IClientRepository _clientsDbContext;

    public CreateClientCommandHandler(IClientRepository clientsDbContext)
    {
        _clientsDbContext = clientsDbContext;
    }

    public async Task<Unit> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new ClientCreateDTO
        {
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            Patronymic = request.Patronymic,
            PhoneNumber = request.PhoneNumber,
            PassportSerie = request.PassportSerie,
            PassportNumber = request.PassportNumber,
            TotalIncomePerMounth = request.TotalIncomePerMounth
        };

        await _clientsDbContext.CreateClient(client, cancellationToken); 
        return Unit.Value;
    }
}
