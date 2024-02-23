
namespace Bank.Domain.Worker;

public class Consultant : Worker
{
    public Consultant()
    {
        DataAccess = new RoleDataAccess(
            new CommandsAccess
            {
                AddClient = false,
                EditClient = true,
                DelClient = false
            },
            new EditFieldsAccess()
            {
                FirstName = false,
                LastName = false,
                MiddleName = false,
                PassportData = false,
                PhoneNumber = true,
                TotalIncome = false
            });
    }  

    public override string ToString()
    {
        return "Консультант";
    }
}

