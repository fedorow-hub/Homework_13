using System;

namespace Homework_13.Models.Client;

public class Client
{
    Guid id;
    private string firstname;
    private string lastname;
    private string patronymic;
    private PhoneNumber phoneNumber;
    private PassportSerie passportSerie;
    private PassportNumber passportNumber;

    public Guid Id { get { return id; } set { id = value; } }

    public string Firstname { get { return firstname; } set { firstname = value; } }

    public string Lastname { get { return lastname; } set { lastname = value; } }

    public string Patronymic { get { return patronymic; } set { patronymic = value; } }

    public PhoneNumber PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }

    public PassportSerie PassportSerie
    {
        get { return passportSerie; }
        set { passportSerie = value; }
    }

    public PassportNumber PassportNumber
    {
        get { return passportNumber; }
        set { passportNumber = value; }
    }

    public Client() { }

    public Client(string firstname, string lastname, string patronymic,
        PhoneNumber phoneNumber, PassportSerie passportSerie, PassportNumber passportNumber)
    {
        this.firstname = firstname;
        this.lastname = lastname;
        this.patronymic = patronymic;
        this.phoneNumber = phoneNumber;
        this.passportSerie = passportSerie;
        this.passportNumber = passportNumber;
    }
}

public class ClientDTO
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Patronymic { get; set; }

    public string PhoneNumber { get; set; }

    public string PassportSerie { get; set; }

    public string PassportNumber { get; set; }
}
