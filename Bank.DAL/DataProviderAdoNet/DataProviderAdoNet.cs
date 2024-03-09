using AutoMapper.Execution;
using Bank.Application.Accounts;
using Bank.Application.Clients.Queries.GetClientList;
using Bank.Application.Interfaces;
using Bank.Domain.Account;
using Bank.Domain.Bank;
using Bank.Domain.Client;
using Bank.Domain.Worker;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Data.Common;
using System.Data.OleDb;

namespace Bank.DAL.DataProviderAdoNet;

public class DataProviderAdoNet : IDataProvider
{
    private readonly DbProviderFactory _provider;
    private readonly ConnectionString _connectionString;
    public DataProviderAdoNet(DbProviderFactory providerFactory, ConnectionString connectionString)
    {
        _provider = providerFactory;
        _connectionString = connectionString;
    }
    public bool CreateBank(SomeBank bank)
    {
        using (DbConnection connection = _provider.CreateConnection())
        {
            if (connection == null)
            {
                return false;
            }

            connection.ConnectionString = _connectionString.String;
            connection.Open();

            DbCommand command = _provider.CreateCommand();
            if (command == null)
            {
                
                return false;
            }
                        
            command.Connection = connection;
                        
            command.CommandText = $@"INSERT INTO [dbo].[Bank] (Id, Name, Capital, DateOfCreation) VALUES(@Id, @Name, @Capital, @DateOfCreation)";

            if(_provider is SqlClientFactory)
            {
                SqlParameter sqlIdParam = new SqlParameter("@Id", bank.Id);
                command.Parameters.Add(sqlIdParam);

                SqlParameter sqlNameParam = new SqlParameter("@Name", bank.Name);
                command.Parameters.Add(sqlNameParam);

                SqlParameter sqlCapitalParam = new SqlParameter("@Capital", bank.Capital);
                command.Parameters.Add(sqlCapitalParam);

                SqlParameter sqlDateCreationParam = new SqlParameter("@DateOfCreation", bank.DateOfCreation.Date.ToString("O"));
                command.Parameters.Add(sqlDateCreationParam);
            }
            else if (_provider is OleDbFactory)
            {
                OleDbParameter idParam = new OleDbParameter("@Id", bank.Id);
                command.Parameters.Add(idParam);

                OleDbParameter oleDbNameParam = new OleDbParameter("@Name", bank.Name);
                command.Parameters.Add(oleDbNameParam);

                OleDbParameter capitalParam = new OleDbParameter("@Capital", bank.Capital);
                command.Parameters.Add(capitalParam);

                OleDbParameter dateCreationParam = new OleDbParameter("@DateOfCreation", bank.DateOfCreation.Date.ToString("O"));
                command.Parameters.Add(dateCreationParam);
            }

            command.ExecuteNonQuery();
            return true;
        }
    }

    public SomeBank GetBank()
    {
        using (DbConnection connection = _provider.CreateConnection())
        {
            if (connection == null)
            {
                return null;
            }

            connection.ConnectionString = _connectionString.String;
            connection.Open();

            DbCommand command = _provider.CreateCommand();
            if (command == null)
            {
                return null;
            }

            Console.WriteLine($"Your command object is a: {command.GetType().Name}");
            command.Connection = connection;

            if (_provider is SqlClientFactory)
            {
                command.CommandText = $@"SELECT * FROM [dbo].[Bank]";
            }
            else if (_provider is OleDbFactory)
            {
                command.CommandText = $@"SELECT * FROM Bank";
            }

            SomeBank bank = null;

            try
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        bank = SomeBank.CreateBank(new Guid(reader.GetString(0)), reader.GetString(1), Convert.ToDecimal(reader.GetString(2)), Convert.ToDateTime(reader.GetString(3)));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            } 
            return bank;
        }
    }

    public bool CreateClient(Client client)
    {
        throw new NotImplementedException();
    }
    public bool UpdateClient(Client client)
    {
        throw new NotImplementedException();
    }

    public bool DeleteClient(Client client)
    {
        throw new NotImplementedException();
    }
    public ClientListVm GetClientList()
    {
        throw new NotImplementedException();
    }

    public bool CreateAccount(Account account)
    {
        throw new NotImplementedException();
    }
    public bool CloseAccount(Guid id)
    {
        throw new NotImplementedException();
    }
    public bool AddMoneyToAccount(Guid id, decimal amount)
    {
        throw new NotImplementedException();
    }
    public bool WithdrawMoneyFromAccount(Guid id, decimal amount)
    {
        throw new NotImplementedException();
    }
    public bool TransactionBetweenAccounts(Guid idAccountFrom, Guid idAccountTo, decimal amount)
    {
        throw new NotImplementedException();
    }

    public AccountListVm GetAccountList()
    {
        throw new NotImplementedException();
    }
}
