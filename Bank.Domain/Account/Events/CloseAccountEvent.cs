using MediatR;

namespace Bank.Domain.Account.Events;

public class CloseAccountEvent : INotification
{
    public Guid Id { get; init; }
}

