
namespace Bank.Domain.Worker;

public class RoleDataAccess
{
    public CommandsAccess Commands;
    public EditFieldsAccess EditFields;

    public RoleDataAccess(CommandsAccess commands, EditFieldsAccess editFields)
    {
        Commands = commands;
        EditFields = editFields;
    }
}

public struct CommandsAccess
{
    public bool AddClient;
    public bool DelClient;
    public bool EditClient;
}

public struct EditFieldsAccess
{
    public bool FirstName;
    public bool LastName;
    public bool MiddleName;
    public bool PhoneNumber;
    public bool PassportData;
    public bool TotalIncome;
}

