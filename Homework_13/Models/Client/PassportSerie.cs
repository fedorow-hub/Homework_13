namespace Homework_13.Models.Client;

public class PassportSerie
{    
    public string Serie { get; set; }

    /// <summary>
    /// Проверка, являются ли вводимые данные серией паспорта
    /// </summary>
    /// <param name="series">Серия</param>
    /// <returns></returns>
    public static bool IsSeries(string value)
    {        
        if (value.Length < 2 || value.Length > 4)
        {
            return false;
        }
        return true;
    }

    public PassportSerie() { }

    /// <summary>
    /// Создаем пасспорт с серией и номером
    /// </summary>
    /// <param name="series">Серия</param>
    /// <param name="number">Номер</param>
    public PassportSerie(string serie)
    {
        Serie = serie;        
    }

    public override string ToString()
    {
        return $"{Serie}";
    }
}
