using Bank.Application.Clients.Queries.GetClientDetails;
using Bank.Application.Clients.Queries.GetClientList;
using Bank.Application.Common.Exeptions;
using Bank.Application.Interfaces;
using Bank.Domain.Client;
using MediatR;

namespace Bank.Application.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
{
    private readonly IClientRepository _clientsDbContext;

    public UpdateClientCommandHandler(IClientRepository clientsDbContext)
    {
        _clientsDbContext = clientsDbContext;
    }

    //public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    //{
    //    var targetClient = await _clientsDbContext.GetClient(request.Id, cancellationToken);
    //    if (targetClient == null) 
    //    {
    //        throw new NotFoundException(nameof(Client), request.Id);
    //    }
                
    //    var client = new ClientUpdateDTO
    //    {
    //        Id = request.Id,
    //        Firstname = request.Firstname,
    //        Lastname = request.Lastname,
    //        Patronymic = request.Patronymic,
    //        PhoneNumber = request.PhoneNumber,
    //        PassportSerie = request.PassportSerie,
    //        PassportNumber = request.PassportNumber,
    //        TotalIncomePerMounth = request.TotalIncomePerMounth
    //    };

    //    await _clientsDbContext.UpdateClient(client, cancellationToken);
    //    return Unit.Value;
    //}

    public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var targetClient = await _clientsDbContext.GetClient(request.Id, cancellationToken);
        if (targetClient == null)
        {
            throw new NotFoundException(nameof(Client), request.Id);
        }

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
        return;
    }
}
