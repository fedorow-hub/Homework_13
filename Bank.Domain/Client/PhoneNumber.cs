using System.Text.RegularExpressions;

namespace Bank.Domain.Client;

public class PhoneNumber : ValueObject
{
    public string Number { get; }
    private PhoneNumber(string number)
    {
        Number = number;
    }

    public static PhoneNumber SetNumber(string number)
    {
        CheckNumber(number);
        return new PhoneNumber(number);
    }

    /// <summary>
    /// Проверка валидности входных данных
    /// </summary>
    /// <param name="number"></param>
    private static void CheckNumber(string number)
    {
        if (string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number))
        {
            throw new ArgumentException($"Номер \"{nameof(number)}\" не может быть пустым или пробелом");
        }

        if (!IsPhoneNumber(number))
        {
            throw new ArgumentException($"Строка \"{nameof(number)}\" не является номером телефона");
        }
    }

    /// <summary>
    /// Проверяем, является ли вводимая строка номером телефона
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsPhoneNumber(string number)
    {
        if (Regex.Match(number, @"^(\+[0-9]{11})$").Success || Regex.Match(number, @"^([0-9]{11})$").Success)
            return true;
        return false;
    }

    protected override IEnumerable<object> GetEqalityComponents()
    {
        yield return Number;
    }

    public override string ToString()
    {
        return $"{Number}";
    }
}
