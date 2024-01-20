using Bank.Application.Interfaces;
using Bank.Domain.Bank;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Bank.DAL
{
    public class BankRepository : IBankRepository
    {
        private readonly string _connectionString;
        public BankRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Task ChangeCapital(SomeBank bank)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Createbank(SomeBank bank)
        {
            using(IDbConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $@"INSERT INTO Bank (Name, Capital, DateOfCreation) 
                                        values ({bank.Name}, {bank.Capital}, {bank.DateOfCreation})";
                command.CommandType = CommandType.Text;
                int affected = command.ExecuteNonQuery();
                return affected;
            }
        }

        public Task<SomeBank> GetBank()
        {
            throw new NotImplementedException();
        }
    }
}
