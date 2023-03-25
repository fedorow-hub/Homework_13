﻿using System;

namespace Homework_13.Models.Client;

public class Client
{
    Guid id;
    private string firstname;
    private string lastname;
    private string patronymic;
    private PhoneNumber phoneNumber;
    private Passport seriesAndNumberOfPassport;

    public Guid Id { get { return id; } set { id = value; } }

    public string Firstname { get { return firstname; } set { firstname = value; } }

    public string Lastname { get { return lastname; } set { lastname = value; } }

    public string Patronymic { get { return patronymic; } set { patronymic = value; } }

    public PhoneNumber PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }

    public Passport SeriesAndNumberOfPassport
    {
        get { return seriesAndNumberOfPassport; }
        set { seriesAndNumberOfPassport = value; }
    }

    public Client() { }

    public Client(string firstname, string lastname, string patronymic,
        PhoneNumber phoneNumber, Passport seriesAndNumberOfPassport)
    {
        this.firstname = firstname;
        this.lastname = lastname;
        this.patronymic = patronymic;
        this.phoneNumber = phoneNumber;
        this.seriesAndNumberOfPassport = seriesAndNumberOfPassport;
    }
}
