namespace ForExperiments2;

public class Account
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int ClientId { get; set; }
    public string Currency { get; set; }
    public bool IsDeposite { get; set; }
    public DateOnly OpeningDate { get; set; }
}
