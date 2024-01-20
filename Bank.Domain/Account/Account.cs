using Bank.Domain.Root;

namespace Bank.Domain.Account;

public abstract class Account : Entity
{    
    /// <summary>
    /// идентификационный номер клиента, которому принадлежит счет
    /// </summary>
    public Guid ClientId { get; private set; }

    /// <summary>
    /// дата и время создания счета
    /// </summary>
    public DateTime TimeOfCreated { get; private set; }

    /// <summary>
    /// сумма, лежащая на счете
    /// </summary>
    public decimal Amount { get; protected set; }

    /// <summary>
    /// дата, до которой действует счет
    /// </summary>
    public DateTime AccountTerm { get; private set; } = DateTime.MaxValue;

    /// <summary>
    /// действующий или закрытый счет
    /// </summary>
    public bool IsExistance { get; private set; }

    public Account()
    {
        
    }

    public Account(Guid clientId, byte termOfMonth, decimal amount, DateTime timeOfCreated)        
    {
        AccountTerm = TimeOfCreated.AddMonths(termOfMonth);
        ClientId = clientId;
        TimeOfCreated = timeOfCreated;
        Amount = amount;
        IsExistance = true;
    }

    public Account(Guid id, Guid clientId, byte termOfMonth, decimal amount, DateTime timeOfCreated)
        :base(id)        
    {
        AccountTerm = TimeOfCreated.AddMonths(termOfMonth);
        ClientId = clientId;
        TimeOfCreated = timeOfCreated;
        Amount = amount;
        IsExistance = true;
    }     
       
    /// <summary>
    /// закрытие счета
    /// </summary>
    /// <exception cref="DomainExeption"></exception>
    public void CloseAccount()
    {
        if(!IsExistance)
        {
            throw new DomainExeption("Данный счет не доступен");
        }
        else if(Amount > 0) 
        {
            throw new DomainExeption("На счете имеются денежные средства");
        }        
        IsExistance = false;        
    }

    /// <summary>
    /// пополнение счета
    /// </summary>
    /// <param name="money"></param>
    public virtual void AddMoneyToAccount(decimal money)
    {
        Amount += money;
    }

    /// <summary>
    /// снятие денег со счета
    /// </summary>
    /// <param name="money"></param>
    public virtual void WithdrawalMoneyFromAccount(decimal money)
    {
        if (Amount >= money)
        {
            Amount -= money;
        }
        else throw new DomainExeption("Недостаточно средств на счете");
    }
}
