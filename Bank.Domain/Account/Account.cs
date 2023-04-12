using Bank.Domain.Root;

namespace Bank.Domain.Account;

public abstract class Account : Entity
{
    /// <summary>
    /// идентификационный номер счета
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// идентификационный номер клиента, которому принадлежит счет
    /// </summary>
    public Guid ClientId { get; }

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
    public decimal Amount { get; private set; }

    /// <summary>
    /// действующий или закрытый счет
    /// </summary>
    public bool IsExistance { get; private set; }

    public Account(Guid clientId, string currency, decimal amount = 0 )
    {
        Id = Guid.NewGuid();
        ClientId = clientId;
        TimeOfCreated = DateTime.Today;
        Currency = Currency.Parse(currency);
        Amount = amount;        
        IsExistance = true;
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
}
