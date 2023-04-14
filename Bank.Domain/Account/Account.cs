using Bank.Domain.Root;

namespace Bank.Domain.Account;

public abstract class Account : Entity
{    
    /// <summary>
    /// идентификационный номер клиента, которому принадлежит счет
    /// </summary>
    public long ClientId { get; }

    /// <summary>
    /// дата и время создания счета
    /// </summary>
    public DateTime TimeOfCreated { get; }

    /// <summary>
    /// валюта счета
    /// </summary>
    public Currency Currency { get; }

    /// <summary>
    /// сумма, лежащая на счете
    /// </summary>
    public decimal Amount { get; protected set; }

    /// <summary>
    /// дата, до которой действует счет
    /// </summary>
    public DateTime AccountTerm { get; protected set; } = DateTime.MaxValue;

    /// <summary>
    /// действующий или закрытый счет
    /// </summary>
    public bool IsExistance { get; private set; }

    public Account(long clientId, string currency, decimal amount)        
    {
        ClientId = clientId;
        TimeOfCreated = DateTime.Today;
        Currency = Currency.Parse(currency);
        Amount = amount;
        IsExistance = true;
    }

    public Account(int id, long clientId, string currency, decimal amount, DateTime timeOfCreated)
        :base(id)        
    {
        ClientId = clientId;
        TimeOfCreated = timeOfCreated;
        Currency = Currency.Parse(currency);
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
    public void AddMoneyToAccount(decimal money)
    {
        Amount += money;
    }

    /// <summary>
    /// снятие денег со счета
    /// </summary>
    /// <param name="money"></param>
    public void WithdrawalMoneyFromAccount(decimal money)
    {
        if (Amount >= money)
        {
            Amount -= money;
        }
        else throw new DomainExeption("Недостаточно средств на счете");
    }
}
