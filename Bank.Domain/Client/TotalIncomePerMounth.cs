using System.Text.RegularExpressions;

namespace Bank.Domain.Client;

public class TotalIncomePerMounth : ValueObject
{
    public decimal Income { get; }
    private TotalIncomePerMounth(decimal income)
    {
        Income = income;
    }

    public static TotalIncomePerMounth SetIncome(string number)
    {        
        bool success = decimal.TryParse(number,  out decimal result);
        if(success)
        {            
            return new TotalIncomePerMounth(result);
        }
        throw new ArgumentException($"Сумма \"{nameof(number)}\" не является корректной суммой дохода");
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
