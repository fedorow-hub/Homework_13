using BankDAL.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace BankDAL.DataOperations;

public class ClientDAL : IDisposable, IClientDAL
{
    private readonly string _connectionString;

    public ClientDAL()
        => _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MSSQLLocalBank;User ID=EvelUser;Password=MAer435N;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    //=>_connectionString = ConfigurationManager.ConnectionStrings["BankSqlProvider"].ConnectionString;

    private SqlConnection _sqlConnection = null;
    private void OpenConnection()
    {
        _sqlConnection = new SqlConnection
        {
            ConnectionString = _connectionString
        };
        _sqlConnection.Open();
    }

    private void CloseConnection()
    {
        if (_sqlConnection?.State != ConnectionState.Closed)
        {
            _sqlConnection?.Close();
        }
    }

    bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }
        if (disposing)
        {
            _sqlConnection.Dispose();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    //public ObservableCollection<Client> GetAllClients()
    //{
    //    OpenConnection();

    //    ObservableCollection<Client> clients = new ObservableCollection<Client>();

    //    string sql = @"SELECT * FROM Clients";

    //    using SqlCommand command = new SqlCommand(sql, _sqlConnection)
    //    {
    //        CommandType = CommandType.Text
    //    };

    //    SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
    //    while (dataReader.Read())
    //    {
    //        clients.Add(new Client
    //        {
    //            Id = (int)dataReader["id"],
    //            Firstname = (string)dataReader["FirstName"],
    //            Lastname = (string)dataReader["LastName"],
    //            Patronymic = (string)dataReader["Patronymic"],
    //            PhoneNumber = (string)dataReader["PhoneNumber"],
    //            SeriePassport = (string)dataReader["SeriePasport"],
    //            NumberPassport = (string)dataReader["NumberPassport"]
    //        });
    //    }

    //    dataReader.Close();
    //    CloseConnection();
    //    return clients;
    //}

    public ObservableCollection<ClientViewModel> GetAllClients()
    {
        OpenConnection();

        ObservableCollection<ClientViewModel> clients = new ObservableCollection<ClientViewModel>();

        string sql = @"SELECT * FROM Clients";

        using SqlCommand command = new SqlCommand(sql, _sqlConnection)
        {
            CommandType = CommandType.Text
        };

        SqlDataReader dataReader = command.ExecuteReader();
        while (dataReader.Read())
        {
            clients.Add(new ClientViewModel
            {
                Id = (int)dataReader["id"],
                Firstname = (string)dataReader["FirstName"],
                Lastname = (string)dataReader["LastName"],
                Patronymic = (string)dataReader["Patronymic"],
                PhoneNumber = (string)dataReader["PhoneNumber"],
                SeriePassport = (string)dataReader["SeriePasport"],
                NumberPassport = (string)dataReader["NumberPassport"]
            });
        }

        List<Account> accounts = new List<Account>();

        sql = @"SELECT * FROM Accounts";

        using SqlCommand commandAccounts = new SqlCommand(sql, _sqlConnection)
        {
            CommandType = CommandType.Text
        };
        dataReader.Close();

        dataReader = commandAccounts.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            accounts.Add(new Account
            {
                Id = (int)dataReader["id"],
                Amount = (decimal)dataReader["Amount"],
                ClientId = (int)dataReader["ClientId"],
                Currency = (string)dataReader["Currency"],
                IsDeposite = (bool)dataReader["IsDeposite"],
                OpeningDate = (DateTime)dataReader["OpeningDate"]
            });
        }

        dataReader.Close();

        for (int i = 0; i < clients.Count; i++)
        {
            clients[i].Accounts = new List<Account>();
            for (int j = 0; j < accounts.Count; j++)
            {
                if (clients[i].Id == accounts[j].ClientId)
                {
                    clients[i].Accounts.Add(accounts[j]);
                }
            }
        }

        dataReader.Close();
        CloseConnection();
        return clients;
    }
}
