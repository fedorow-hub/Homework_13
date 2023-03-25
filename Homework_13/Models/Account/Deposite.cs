using Homework_13.Models.Money;
using System;

namespace Homework_13.Models.Account;

public class Deposite<T> : Account<T>
    where T : Currency, new()
{
    public byte TermInMounth { get; private set; }
    public byte InterestRate { get; private set; }

    public decimal AmountOfDeposit { get; private set; }

    public DateTime DepositeOpeningDate { get; private set; }

    public Deposite(T amount, byte mounth)
        : base(amount)
    {
        TermInMounth = mounth;
        InterestRate = GetInterestRate(amount, mounth);
        DepositeOpeningDate = DateTime.UtcNow;
        AmountOfDeposit = amount.Money;
    }
    private byte GetInterestRate(T amount, byte mounth)
    {
        if (mounth > 12)
        {
            if (amount.Money > 100000)
            {
                return Convert.ToByte(Rate.large);
            }
            else if (amount.Money > 50000)
            {
                return Convert.ToByte(Rate.medium);
            }
            else
            {
                return Convert.ToByte(Rate.small);
            }
        }
        else
        {
            return Convert.ToByte(Rate.small);
        }
    }

    private enum Rate
    {
        small = 3,
        medium = 5,
        large = 10
    }

    public override void SetMoney(T amount)
    {
        throw new ArgumentException("Депозит не пополняемый");
    }

    public override T GetMoney(decimal amount)
    {
        throw new ArgumentException("Снять часть денег с депозита нельзя, возможно только закрытие счета");
    }
}
