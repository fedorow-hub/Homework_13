namespace Bank.Application.Interfaces;

public interface IExchangeRateService
{
    public string GetDate();
    public (decimal prev, decimal cur) GetDollarExchangeRate();
    public (decimal prev, decimal cur) GetEuroExchangeRate();
}
