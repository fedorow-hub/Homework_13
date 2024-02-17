using Bank.Domain.Root;

namespace Bank.Domain.Client.ValueObjects;

public sealed class PassportNumber : ValueObject
{
    public const int MinNumberValue = 100;
    public const int MaxNumberValue = 999999;

    public string Number { get; }

    private PassportNumber(string number)
    {
        Number = number;
    }

    public static PassportNumber SetNumber(string number)
    {
        if (!IsNumber(number))
        {
            throw new ArgumentException($"Номер \"{nameof(number)}\" не является номером паспорта");
        }
        return new PassportNumber(number);
    }

    /// <summary>
    /// Проверка, являются ли вводимые данные номером паспорта
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNumber(string value)
    {
        if (!int.TryParse(value, out var number))
            return false;

        return number is >= MinNumberValue and <= MaxNumberValue;
    }

    public override string ToString()
    {
        return $"{Number}";
    }

    protected override IEnumerable<object> GetEqalityComponents()
    {
        yield return Number;
    }
}
