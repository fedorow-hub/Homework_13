namespace Bank.Domain.Client;

public sealed class TotalIncomePerMounth : ValueObject
{
    public decimal Income { get; }
    private TotalIncomePerMounth(decimal income)
    {
        Income = income;
    }

    public static TotalIncomePerMounth SetIncome(string number)
    {
        bool success = decimal.TryParse(number, out decimal result);
        if (success && result > 0)
        {
            return new TotalIncomePerMounth(result);
        }
        throw new ArgumentException($"Сумма \"{nameof(number)}\" не является корректной суммой дохода");
    }


    public static bool IsIncome(string value)
    {
        int number;

        if (!int.TryParse(value, out number))
            return false;

        if (number < 0)
        {
            return false;
        }

        return true;
    }



    protected override IEnumerable<object> GetEqalityComponents()
    {
        yield return Income;
    }

    public override string ToString()
    {
        return $"{Income}";
    }
}
