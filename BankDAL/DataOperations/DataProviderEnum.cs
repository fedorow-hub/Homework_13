namespace BankDAL.DataOperations;

//OleDb is Windows only and is not supported in .NET Core
public enum DataProviderEnum
{
    SqlServer,
#if PC
    OleDb,
#endif
    None
}
