namespace BankDAL.Models;

public class Account
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int ClientId { get; set; }
    public string Currency { get; set; }
    public bool IsDeposite { get; set; }
    public DateTime OpeningDate { get; set; }
}
