using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;

namespace BankDAL.DataOperations;

public class DBProvFactory
{
    public DbProviderFactory Factory { get; }
    public DBProvFactory()
    {
        Factory = GetDbProviderFactory(provider);
    }
    string connectionString = ConfigurationManager.ConnectionStrings["BankSqlProvider"].ConnectionString;

    DataProviderEnum provider = GetProviderFromConfiguration();



    DbProviderFactory GetDbProviderFactory(DataProviderEnum provider)
        => provider switch
        {
            DataProviderEnum.SqlServer => SqlClientFactory.Instance,
#if PC
            DataProviderEnum.OleDb => OleDbFactory.Instance,
#endif
            _ => null
        };

    static DataProviderEnum GetProviderFromConfiguration()
    {
        string dataProvider = ConfigurationManager.AppSettings["provider"];

        if (Enum.TryParse<DataProviderEnum>(dataProvider, out DataProviderEnum provider))
        {
            return provider;
        };
        throw new Exception("Invalid data provider value supplied.");
    }

    //подключение к OleDB
    //var connToAccess = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source= D:\ADO.NET\DATA\Access.mdb");

    //var connToExcel = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source= D:\ADO.NET\DATA\Excel.xls; Extended Properties=""Excel 8.0"""); - Excel 8.0 это версия файла Excel
}
