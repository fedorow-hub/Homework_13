namespace Bank.Domain.Client;

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
        if(!IsNumber(number))
        {
            throw new ArgumentException($"Номер \"{nameof(number)}\" не является номером паспорта");
        };
        return new PassportNumber(number);
    }

    /// <summary>
    /// Проверка, являются ли вводимые данные номером паспорта
    /// </summary>
    /// <param name="number">Номер</param>
    /// <returns></returns>
    public static bool IsNumber(string value)
    {
        int number;

        if (!int.TryParse(value, out number))
            return false;

        if (number < MinNumberValue || number > MaxNumberValue)
        {
            return false;
        }
        return true;
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
