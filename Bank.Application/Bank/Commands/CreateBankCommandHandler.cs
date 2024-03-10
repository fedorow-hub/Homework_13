using Bank.Application.Interfaces;
using MediatR;
using Bank.Domain.Bank;

namespace Bank.Application.Bank.Commands;

public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, SomeBank>
{
    private readonly IDataProvider _dataProvider;

    public CreateBankCommandHandler(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task<SomeBank> Handle(CreateBankCommand request, CancellationToken cancellationToken)
    {

        var entity = _dataProvider.GetBank();

        var bank = SomeBank.CreateBank(request.Id, request.Name!, request.Capital, request.DateOfCreation);

        if (entity != null) return entity;
        _dataProvider.CreateBank(bank);
        return bank;
    }
}
