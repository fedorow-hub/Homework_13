using Bank.Application.Interfaces;
using Bank.Domain.Account.Events;
using MediatR;
using Serilog;

namespace Bank.Application.Accounts.Commands.AddAndWithdrawalMoney;

public class AddMoneyToAccountEventHandler : INotificationHandler<AddedMoneyToAccountEvent>
{
    private readonly ICurrentWorkerService _workerService;
    public AddMoneyToAccountEventHandler(ICurrentWorkerService workerService)
    {
        _workerService = workerService;
    }

    public Task Handle(AddedMoneyToAccountEvent notification, CancellationToken cancellationToken)
    {
        Log.Information($"На счет {notification.Id} {_workerService.Worker} внес {notification.AddedMoney} рублей");
        return Task.CompletedTask;
    }
}

