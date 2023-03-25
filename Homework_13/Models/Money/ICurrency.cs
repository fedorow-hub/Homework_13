namespace Homework_13.Models.Money;

public interface ICurrency<out T>
    where T : Currency
{
    T GetValue { get; }
    T GetValueMethod();
}
