namespace Homework_13.Models.Money;

public class Dollar : Currency, ICurrency<Currency>
{
    public Currency GetValue { get { return new Currency(); } }

    public Currency GetValueMethod()
    {
        return new Currency();
    }
}
