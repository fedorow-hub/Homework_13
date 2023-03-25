namespace BankDAL.Models;

public class Client
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Patronymic { get; set; }
    public string PhoneNumber { get; set; }
    public string SeriePassport { get; set; }
    public string NumberPassport { get; set; }
}
