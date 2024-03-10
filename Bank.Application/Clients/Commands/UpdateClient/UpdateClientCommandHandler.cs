using System.Globalization;
using Bank.Application.Interfaces;
using MediatR;

namespace Bank.Application.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
{
    private readonly IDataProvider _dataProvider;

    public UpdateClientCommandHandler(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var client = _dataProvider.GetClient(request.Id);

        client?.ChangeFirstname(request.Firstname);
        client?.ChangeLastname(request.Lastname);
        client?.ChangePatronymic(request.Patronymic);
        client?.ChangePhoneNumber(request.PhoneNumber);
        client?.ChangePassportSeries(request.PassportSeries);
        client?.ChangePassportNumber(request.PassportNumber);
        client?.ChangeTotalIncomePerMounth(request.TotalIncomePerMounth.ToString(CultureInfo.CurrentCulture));

        bool isSuccess = _dataProvider.UpdateClient(client);

        if (isSuccess)
        {
            client?.AddDomainEvent(new UpdateClientEvent
            {
                Id = request.Id
            });
        }
    }
}
