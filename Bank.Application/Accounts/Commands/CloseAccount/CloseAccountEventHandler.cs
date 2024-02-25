using Bank.Application.Interfaces;
using Bank.Domain.Account.Events;
using MediatR;
using Serilog;

namespace Bank.Application.Accounts.Commands.CloseAccount
{
    public class CloseAccountEventHandler : INotificationHandler<CloseAccountEvent>
    {
        private readonly ICurrentWorkerService _workerService;
        public CloseAccountEventHandler(ICurrentWorkerService workerService)
        {
            _workerService = workerService;
        }
        public Task Handle(CloseAccountEvent notification, CancellationToken cancellationToken)
        {
            Log.Information($"{_workerService.Worker} закрыл счет {notification.Id}");
            return Task.CompletedTask;
        }
    }
}
