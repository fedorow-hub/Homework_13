﻿using Bank.Application.Interfaces;
using Bank.Domain.Account;
using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount.CreatePlainAccount;

public class CreatePlainAccountCommandHandler : IRequestHandler<CreatePlainAccountCommand>
{
    private readonly IAccountRepository _accountRepository;

    public CreatePlainAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    //public async Task<Unit> Handle(CreatePlainAccountCommand request, CancellationToken cancellationToken)
    //{
    //    var plainAccount = PlainAccount.CreatePlaneAccount(request.ClientId, request.Currency, request.Amount);
    //    var accountDTO = new PlainAccountCreateDTO
    //    {
    //        ClientId = plainAccount.ClientId,
    //        Currency = plainAccount.Currency.Name,
    //        Amount = plainAccount.Amount,
    //    };
    //    await _accountRepository.CreatePlainAccount(accountDTO, cancellationToken);
    //    return Unit.Value;
    //}

    public async Task Handle(CreatePlainAccountCommand request, CancellationToken cancellationToken)
    {
        var plainAccount = PlainAccount.CreatePlaneAccount(request.ClientId, DateTime.Now, request.TermOfMonth, request.Amount);
        var accountDTO = new PlainAccountCreateDTO
        {
            ClientId = plainAccount.ClientId,
            Amount = plainAccount.Amount,
        };
        await _accountRepository.CreatePlainAccount(accountDTO, cancellationToken);
        return;
    }
}
