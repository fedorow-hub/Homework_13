using Bank.Application.Common.Exeptions;
using Bank.Application.Interfaces;
using Bank.Domain;
using Bank.Domain.Bank;
using Bank.Domain.Client;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Clients.Commands.CreateClient;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateClientCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var bank = await _dbContext.Bank.FirstOrDefaultAsync(cancellationToken);

        var client = new Client(request.Id, request.Firstname, request.Lastname, request.Patronymic, request.PhoneNumber, request.PassportSerie, request.PassportNumber, request.TotalIncomePerMounth, bank);
    
        await _dbContext.Clients.AddAsync(client);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return;
    }
}
