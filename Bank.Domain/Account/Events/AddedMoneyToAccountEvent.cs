using MediatR;

namespace Bank.Domain.Account.Events;

public sealed record AddedMoneyToAccountEvent : INotification
{
    public Guid Id { get; init; }
    public decimal AddedMoney { get; init; }
}

