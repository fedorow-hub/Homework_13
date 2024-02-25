
using MediatR;

namespace Bank.Application.Accounts.Commands.CreateAccount;

public class CreateAccountEvent : INotification
{
    public Guid Id { get; init; }
    public decimal Money { get; init; }
}

