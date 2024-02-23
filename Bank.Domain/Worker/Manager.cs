
namespace Bank.Domain.Worker;

public class Manager : Worker
{
    public Manager()
    {

        DataAccess = new RoleDataAccess(
            new CommandsAccess
            {
                AddClient = true,
                EditClient = true,
                DelClient = true
            },
            new EditFieldsAccess()
            {
                FirstName = true,
                LastName = true,
                MiddleName = true,
                PassportData = true,
                PhoneNumber = true,
                TotalIncome = true
            });
    }

    public override string ToString()
    {
        return "Менеджер";
    }
}

