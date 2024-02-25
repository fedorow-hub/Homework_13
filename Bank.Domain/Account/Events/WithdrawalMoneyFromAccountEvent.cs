using MediatR;

namespace Bank.Domain.Account.Events
{
    public class WithdrawalMoneyFromAccountEvent : INotification
    {
        public Guid Id { get; init; }
        public decimal WithdrawnMoney { get; init; }
    }
}
