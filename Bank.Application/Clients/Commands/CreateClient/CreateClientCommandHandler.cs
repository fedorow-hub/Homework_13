﻿using Bank.Application.Interfaces;
using Bank.Domain.Client;
using MediatR;

namespace Bank.Application.Clients.Commands.CreateClient;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand>
{
    private readonly IDataProvider _dataProvider;
    public CreateClientCommandHandler(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var bank = _dataProvider.GetBank();

        if (bank != null)
        {
            var client = new Client(request.Id,
                request.Firstname, request.Lastname, request.Patronymic, request.PhoneNumber,
                request.PassportSeries, request.PassportNumber, request.TotalIncomePerMounth, bank.Id);

            _dataProvider.CreateClient(client);
            client.AddDomainEvent(new CreateClientEvent
            {
                Id = client.Id,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Patronymic = request.Patronymic
            });
        }
    }
}
