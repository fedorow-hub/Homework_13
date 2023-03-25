using Homework_13.Models.Money;
using System;

namespace Homework_13.Models.Account;

public abstract class Account<T> : IAccount<T>
    where T : Currency, new()
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public T Amount { get; set; }

    public string Type { get; set; }

    public Account(T amount)
    {
        this.Amount = amount;

        Type = typeof(T).ToString().Substring(25);
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

public abstract class Account : IAccount
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Amount { get; set; }

    public Account()
    {

    }
        
    public Account(decimal amount)
    {
        this.Amount = amount;
    }

    public virtual void SetMoney(decimal amount)
    {
        Amount += amount;
    }

    public virtual void GetMoney(decimal amount)
    {
        if (amount > Amount)
        {
            throw new ArgumentException("Нет столько денег");
        }
        Amount -= amount;        
    }

    public decimal CloseAccount()
    {        
        return Amount;
    }
}