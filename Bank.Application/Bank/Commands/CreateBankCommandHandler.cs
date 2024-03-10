using Bank.Application.Interfaces;
using MediatR;
using Bank.Domain.Bank;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Bank.Application.Bank.Commands;

public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, SomeBank>
{
    //private readonly IApplicationDbContext _dbContext;    

    //public CreateBankCommandHandler(IApplicationDbContext dbContext)
    //{
    //    _dbContext = dbContext;
    //}

    //public async Task<SomeBank> Handle(CreateBankCommand request, CancellationToken cancellationToken)
    //{

    //    var entity = await _dbContext.Bank.FirstOrDefaultAsync(cancellationToken);

    //    var bank = SomeBank.CreateBank(request.Name!, request.Capital, request.DateOfCreation);

    //    if (entity != null) return entity;
    //    await _dbContext.Bank.AddAsync(bank, cancellationToken);
    //    await _dbContext.SaveChangesAsync(cancellationToken);
    //    return bank;
    //}

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
