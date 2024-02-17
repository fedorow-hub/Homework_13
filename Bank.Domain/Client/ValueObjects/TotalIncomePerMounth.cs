using Bank.Domain.Root;
namespace Bank.Domain.Client.ValueObjects;

public sealed class TotalIncomePerMounth : ValueObject
{
    public decimal Income { get; }
    private TotalIncomePerMounth(decimal income)
    {
        Income = income;
    }

    public static TotalIncomePerMounth SetIncome(string number)
    {
        var success = decimal.TryParse(number, out var result);
        if (success && result > 0)
        {
            return new TotalIncomePerMounth(result);
        }
        throw new ArgumentException($"Сумма \"{nameof(number)}\" не является корректной суммой дохода");
    }


    public static bool IsIncome(string value)
    {
        if (!int.TryParse(value, out var number))
            return false;

        return number >= 0;
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
