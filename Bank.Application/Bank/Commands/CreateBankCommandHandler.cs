using Bank.Application.Interfaces;
using MediatR;
using Bank.Domain.Bank;

namespace Bank.Application.Bank.Commands;

public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand>
{
    private readonly IBankRepository _bankRepository;

    public CreateBankCommandHandler(IBankRepository bankRepository)
    {
        _bankRepository = bankRepository;
    }
    //public async Task<Unit> Handle(CreateBankCommand request, CancellationToken cancellationToken)
    //{
    //    var bank = SomeBank.CreateBank(request.Name, request.Capital);

    //    await _bankRepository.Createbank(bank);
    //    return Unit.Value;
    //}

    public async Task Handle(CreateBankCommand request, CancellationToken cancellationToken)
    {
        var bank = SomeBank.CreateBank(request.Name, request.Capital);

        await _bankRepository.Createbank(bank);
        return;
    }
}
