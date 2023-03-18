using Homework_13.Models.Money;
using System;

namespace Homework_13.Models.Account;

public class NotDeposite<T> : Account<T>
    where T : Currency, new()
{  

    public NotDeposite(T amount)
        : base(amount)
    {      
    }
}
