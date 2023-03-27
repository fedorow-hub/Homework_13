namespace Homework_13.Models.Client;

public class PassportNumber
{    
    public const int MinNumberValue = 100;
    public const int MaxNumberValue = 999999;

    public int Number { get; set; }

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

    public PassportNumber() { }

    /// <summary>
    /// Создаем пасспорт с серией и номером
    /// </summary>
    /// <param name="series">Серия</param>
    /// <param name="number">Номер</param>
    public PassportNumber(int number)
    {
        Number = number;
    }

    public override string ToString()
    {
        return $"{Number}";
    }
}
