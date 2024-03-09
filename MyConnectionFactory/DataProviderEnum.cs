
namespace MyConnectionFactory;

public enum DataProviderEnum
{
    SqlServer,
    SqLite,
#if PC
    OleDb,
#endif
    Odbc,
    None
}
