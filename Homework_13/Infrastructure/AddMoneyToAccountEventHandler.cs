using Bank.Domain.Account.Events;
using MediatR;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace Homework_13.Infrastructure;

public class AddMoneyToAccountEventHandler : INotificationHandler<AddedMoneyToAccountEvent>
{
    private readonly IMediator _mediator;
    public AddMoneyToAccountEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public Task Handle(AddedMoneyToAccountEvent notification, CancellationToken cancellationToken)
    {
        Log.Information($"На счет {notification.Id} внесено {notification.AddedMoney} рублей");
        return Task.CompletedTask;
    }
}

