namespace Bank.Application.Interfaces;

public interface IExchangeRateService
{
    public decimal GetDollarExchangeRate();

    public bool IsUSDRateGrow();

    public decimal GetEuroExchangeRate();

    public bool IsEuroRateGrow();    
}
