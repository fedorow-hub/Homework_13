using Homework_13.Models.Money;

namespace Homework_13.Models.Account;

public interface IAccount<T>
    where T : Currency, new()
{
    public void SetMoney(T amount);
    public T GetMoney(decimal amount);
    public T CloseAccount();
}
