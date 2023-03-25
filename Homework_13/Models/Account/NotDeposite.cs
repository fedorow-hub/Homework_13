using Homework_13.Models.Money;

namespace Homework_13.Models.Account;

public class NotDeposite<T> : Account<T>
    where T : Currency, new()
{

    public NotDeposite(T amount)
        : base(amount)
    {
    }
}

public class NotDeposite : Account    
{
    public NotDeposite()
    {

    }
    public NotDeposite(decimal amount)
        : base(amount)
    {
    }
}
