using AutoMapper.Execution;
using Bank.Application.Accounts;
using Bank.Application.Clients.Queries.GetClientList;
using Bank.Application.Interfaces;
using Bank.Domain.Account;
using Bank.Domain.Bank;
using Bank.Domain.Client;
using Bank.Domain.Client.ValueObjects;
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

            if (_provider is SqlClientFactory)
            {
                command.CommandText = $@"INSERT INTO [dbo].[Clients] (Id, Firstname, Lastname, Patronymic, PhoneNumber, PassportSeries, PassportNumber, TotalIncomePerMounth, BankId) 
                                VALUES(@Id, @Firstname, @Lastname, @Patronymic, @PhoneNumber, @PassportSeries, @PassportNumber, @TotalIncomePerMounth, @BankId)";

                SqlParameter sqlIdParam = new SqlParameter("@Id", client.Id);
                command.Parameters.Add(sqlIdParam);

                SqlParameter sqlFirstnameParam = new SqlParameter("@Firstname", client.Firstname.Name);
                command.Parameters.Add(sqlFirstnameParam);

                SqlParameter sqlLastnameParam = new SqlParameter("@Lastname", client.Lastname.Name);
                command.Parameters.Add(sqlLastnameParam);

                SqlParameter sqlPatronymicParam = new SqlParameter("@Patronymic", client.Patronymic.Name);
                command.Parameters.Add(sqlPatronymicParam);

                SqlParameter sqlPhoneNumberParam = new SqlParameter("@PhoneNumber", client.PhoneNumber.Number);
                command.Parameters.Add(sqlPhoneNumberParam);

                SqlParameter sqlPassportSeriesParam = new SqlParameter("@PassportSeries", client.PassportSeries.Series);
                command.Parameters.Add(sqlPassportSeriesParam);

                SqlParameter sqlPassportNumberParam = new SqlParameter("@PassportNumber", client.PassportNumber.Number);
                command.Parameters.Add(sqlPassportNumberParam);

                SqlParameter sqlTotalIncomePerMounthParam = new SqlParameter("@TotalIncomePerMounth", client.TotalIncomePerMounth.Income);
                command.Parameters.Add(sqlTotalIncomePerMounthParam);

                SqlParameter sqlBankIdParam = new SqlParameter("@BankId", client.BankId);
                command.Parameters.Add(sqlBankIdParam);
            }
            else if (_provider is OleDbFactory)
            {
                command.CommandText = $@"INSERT INTO Clients (Id, Firstname, Lastname, Patronymic, PhoneNumber, PassportSeries, PassportNumber, TotalIncomePerMounth, BankId) 
                                VALUES(@Id, @Firstname, @Lastname, @Patronymic, @PhoneNumber, @PassportSeries, @PassportNumber, @TotalIncomePerMounth, @BankId)";

                OleDbParameter sqlIdParam = new OleDbParameter("@Id", client.Id);
                command.Parameters.Add(sqlIdParam);

                OleDbParameter sqlFirstnameParam = new OleDbParameter("@Firstname", client.Firstname.Name);
                command.Parameters.Add(sqlFirstnameParam);

                OleDbParameter sqlLastnameParam = new OleDbParameter("@Lastname", client.Lastname.Name);
                command.Parameters.Add(sqlLastnameParam);

                OleDbParameter sqlPatronymicParam = new OleDbParameter("@Patronymic", client.Patronymic.Name);
                command.Parameters.Add(sqlPatronymicParam);

                OleDbParameter sqlPhoneNumberParam = new OleDbParameter("@PhoneNumber", client.PhoneNumber.Number);
                command.Parameters.Add(sqlPhoneNumberParam);

                OleDbParameter sqlPassportSeriesParam = new OleDbParameter("@PassportSeries", client.PassportSeries.Series);
                command.Parameters.Add(sqlPassportSeriesParam);

                OleDbParameter sqlPassportNumberParam = new OleDbParameter("@PassportNumber", client.PassportNumber.Number);
                command.Parameters.Add(sqlPassportNumberParam);

                OleDbParameter sqlTotalIncomePerMounthParam = new OleDbParameter("@TotalIncomePerMounth", client.TotalIncomePerMounth.Income);
                command.Parameters.Add(sqlTotalIncomePerMounthParam);

                OleDbParameter sqlBankIdParam = new OleDbParameter("@BankId", client.BankId);
                command.Parameters.Add(sqlBankIdParam);                
            }

            command.ExecuteNonQuery();
            return true;
        }
    }
    public bool UpdateClient(Client client)
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

            if (_provider is SqlClientFactory)
            {
                command.CommandText = $@"UPDATE [dbo].[Clients] SET 
                                            Firstname = @Firstname, 
                                            Lastname = @Lastname, 
                                            Patronymic = @Patronymic, 
                                            PhoneNumber = @PhoneNumber,
                                            PassportSeries = @PassportSeries,
                                            PassportNumber = @PassportNumber,
                                            TotalIncomePerMounth = @TotalIncomePerMounth
                                        WHERE Id = '{client.Id}'";

                SqlParameter sqlFirstnameParam = new SqlParameter("@Firstname", client.Firstname.Name);
                command.Parameters.Add(sqlFirstnameParam);

                SqlParameter sqlLastnameParam = new SqlParameter("@Lastname", client.Lastname.Name);
                command.Parameters.Add(sqlLastnameParam);

                SqlParameter sqlPatronymicParam = new SqlParameter("@Patronymic", client.Patronymic.Name);
                command.Parameters.Add(sqlPatronymicParam);

                SqlParameter sqlPhoneNumberParam = new SqlParameter("@PhoneNumber", client.PhoneNumber.Number);
                command.Parameters.Add(sqlPhoneNumberParam);

                SqlParameter sqlPassportSeriesParam = new SqlParameter("@PassportSeries", client.PassportSeries.Series);
                command.Parameters.Add(sqlPassportSeriesParam);

                SqlParameter sqlPassportNumberParam = new SqlParameter("@PassportNumber", client.PassportNumber.Number);
                command.Parameters.Add(sqlPassportNumberParam);

                SqlParameter sqlTotalIncomePerMounthParam = new SqlParameter("@TotalIncomePerMounth", client.TotalIncomePerMounth.Income);
                command.Parameters.Add(sqlTotalIncomePerMounthParam);
            }
            else if (_provider is OleDbFactory)
            {
                var tempClientId = "{" + client.Id + "}"; // Access добавляет фигурные скобки к Id
                command.CommandText = $@"UPDATE Clients SET 
                                            Firstname = @Firstname, 
                                            Lastname = @Lastname, 
                                            Patronymic = @Patronymic, 
                                            PhoneNumber = @PhoneNumber,
                                            PassportSeries = @PassportSeries,
                                            PassportNumber = @PassportNumber,
                                            TotalIncomePerMounth = @TotalIncomePerMounth
                                        WHERE Id = '{tempClientId}'";

                OleDbParameter sqlFirstnameParam = new OleDbParameter("@Firstname", client.Firstname.Name);
                command.Parameters.Add(sqlFirstnameParam);

                OleDbParameter sqlLastnameParam = new OleDbParameter("@Lastname", client.Lastname.Name);
                command.Parameters.Add(sqlLastnameParam);

                OleDbParameter sqlPatronymicParam = new OleDbParameter("@Patronymic", client.Patronymic.Name);
                command.Parameters.Add(sqlPatronymicParam);

                OleDbParameter sqlPhoneNumberParam = new OleDbParameter("@PhoneNumber", client.PhoneNumber.Number);
                command.Parameters.Add(sqlPhoneNumberParam);

                OleDbParameter sqlPassportSeriesParam = new OleDbParameter("@PassportSeries", client.PassportSeries.Series);
                command.Parameters.Add(sqlPassportSeriesParam);

                OleDbParameter sqlPassportNumberParam = new OleDbParameter("@PassportNumber", client.PassportNumber.Number);
                command.Parameters.Add(sqlPassportNumberParam);

                OleDbParameter sqlTotalIncomePerMounthParam = new OleDbParameter("@TotalIncomePerMounth", client.TotalIncomePerMounth.Income);
                command.Parameters.Add(sqlTotalIncomePerMounthParam);
            }
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }            
        }
    }
    public bool DeleteClient(Guid clientId)
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

            if (_provider is SqlClientFactory)
            {
                command.CommandText = $@"DELETE FROM [dbo].[Clients] WHERE Id = '{clientId}'";

            }
            else if (_provider is OleDbFactory)
            {
                var tempClientId = "{" + clientId + "}"; // Access добавляет фигурные скобки к Id
                command.CommandText = $@"DELETE FROM Clients WHERE Id = '{tempClientId}'";
            }

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }            
        }
    }
    public Client GetClient(Guid clientId)
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

            command.Connection = connection;

            if (_provider is SqlClientFactory)
            {
                command.CommandText = $@"SELECT * FROM [dbo].[Clients] WHERE Id = '{clientId}'";
            }
            else if (_provider is OleDbFactory)
            {
                var tempClientId = "{" + clientId + "}"; // Access добавляет фигурные скобки к Id
                command.CommandText = $@"SELECT * FROM Clients WHERE Id = '{tempClientId}'";
            }
            
            try
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows) 
                {
                    while (reader.Read()) 
                    {
                        var client = new Client(new Guid(reader.GetString(0)), reader.GetString(1), reader.GetString(2), 
                            reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), new Guid(reader.GetString(8)));
                        return client;
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            return null;
        }
    }
    public ClientListVm GetClientList()
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

            command.Connection = connection;

            if (_provider is SqlClientFactory)
            {
                command.CommandText = $@"SELECT * FROM [dbo].[Clients]";
            }
            else if (_provider is OleDbFactory)
            {
                command.CommandText = $@"SELECT * FROM Clients";
            }

            SomeBank bank = null;

            var clientList = new ClientListVm();
            clientList.Clients = new List<ClientLookUpDto>();

            try
            {
                var reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var client = new ClientLookUpDto
                        {
                            Id = new Guid(reader.GetString(0)),
                            Firstname = reader.GetString(1),
                            Lastname = reader.GetString(2),
                            Patronymic = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            PassportSeries = reader.GetString(5),
                            PassportNumber = reader.GetString(6),
                            TotalIncomePerMounth = reader.GetString(7)
                        };
                        clientList.Clients.Add(client);
                    }
                }
                return clientList;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return clientList;
            }            
        }
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

    public AccountListVm GetAccountList(Guid clientId)
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

            command.Connection = connection;

            if (_provider is SqlClientFactory)
            {
                command.CommandText = $@"SELECT * FROM [dbo].[Accounts] WHERE ClientId = {clientId}";
            }
            else if (_provider is OleDbFactory)
            {
                command.CommandText = $@"SELECT * FROM Accounts WHERE ClientId = {clientId}";
            }

            var accounts = new AccountListVm();
            accounts.Accounts = new List<Account>();

            try
            {
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Guid id, Guid clientId, byte termOfMonth, decimal amount, DateTime timeOfCreated, TypeOfAccount type
                        //var account = new Account(new Guid(reader.GetString(0)), new Guid(reader.GetString(1)), );
                        
                        //accounts.Accounts.Add(account);
                    }
                }
                return accounts;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return accounts;
            }
        }
    }

    
}
