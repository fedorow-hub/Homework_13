using Homework_13.Models.Money;
using System;

namespace Homework_13.Models.Account;

public abstract class Account<T> : IAccount<T>
    where T : Currency, new()
{
    public Guid Id { get; set; } = Guid.NewGuid();    
    public T Amount { get; private set; }

    public Account(T amount)
    {        
        this.Amount = amount;
    }

    public virtual void SetMoney(T amount)
    {
        Amount.Money += amount.Money;
    }

    public virtual T GetMoney(decimal amount)
    {
        if (amount > Amount.Money)
        {
            throw new ArgumentException("Нет столько денег");
        }
        Amount.Money -= amount;
        T temp = new();
        temp.Money = amount;
        return temp;
    }

    public T CloseAccount()
    {
        T temp = Amount;
        Amount.Money = 0;
        return temp;
    }
}
