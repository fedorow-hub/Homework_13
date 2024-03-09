using System.Data;
using System.Data.Odbc;
using System.Data.Common;
#if PC
using System.Data.OleDb;
#endif
using Microsoft.Data.SqlClient;
using MyConnectionFactory;
using Microsoft.Extensions.Configuration;


var (provider, connectionString) = GetProviderFromConfiguration();

DbProviderFactory factory = GetDbProviderFactory(provider);

using (DbConnection connection = factory.CreateConnection())
{
    if (connection == null)
    {
        Console.WriteLine($"Unable to create the connection object");
        return;
    }

    Console.WriteLine($"Your connection object is a: {connection.GetType().Name}");
    connection.ConnectionString = connectionString;
    connection.Open();

    DbCommand command = factory.CreateCommand();
    if (command == null)
    {
        Console.WriteLine($"Unable to create the command object");
        return;
    }

    Console.WriteLine($"Your command object is a: {command.GetType().Name}");
    command.Connection = connection;

    command.CommandText = @"CREATE TABLE [dbo].[Client]([Id] INT NOT NULL PRIMARY KEY, [Name] NCHAR(10) NULL)";
    command.ExecuteNonQuery();
    Console.ReadLine();
}



static (DataProviderEnum Provider, string ConnectionString) GetProviderFromConfiguration()
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .Build();

    var providerName = config["ProviderName"];
    if (Enum.TryParse<DataProviderEnum>
    (providerName, out DataProviderEnum provider))
    {
        return (provider, config[$"{providerName}:ConnectionString"]);
    };
    throw new Exception("Invalid data provider value supplied.");
}

static DbProviderFactory GetDbProviderFactory(DataProviderEnum provider)
 => provider switch
 {
     DataProviderEnum.SqlServer => SqlClientFactory.Instance,
     DataProviderEnum.SqLite => SqlClientFactory.Instance,
     DataProviderEnum.Odbc => OdbcFactory.Instance,
#if PC
     DataProviderEnum.OleDb => OleDbFactory.Instance,
#endif
     _ => null
 };


