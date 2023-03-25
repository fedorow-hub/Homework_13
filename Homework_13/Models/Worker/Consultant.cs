using Homework_13.Models.Client;

namespace Homework_13.Models.Worker;

internal class Consultant : Worker
{
    public Consultant()
    {
        DataAccess = new RoleDataAccess(
            new CommandsAccess
            {
                AddClient = false,
                EditClient = true,
                DelClient = false,
                OperationAccount = false
            },
            new EditFieldsAccess()
            {
                FirstName = false,
                LastName = false,
                MidleName = false,
                PassortData = false,
                PhoneNumber = true
            });
    }

    public override ClientAccessInfo GetClientInfo(Client.Client client)
    {
        ClientAccessInfo clientAccessInfo = new ClientAccessInfo(client);
        clientAccessInfo.PassportSerie = "****";
        clientAccessInfo.PassportNumber = "*******";

        return clientAccessInfo;
    }
    public override string ToString()
    {
        return "Консультант";
    }
}
