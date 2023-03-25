using Homework_13.Models.Client;

namespace Homework_13.Models.Worker;

internal class Manager : Worker
{
    public Manager()
    {
        DataAccess = new RoleDataAccess(
            new CommandsAccess
            {
                AddClient = true,
                EditClient = true,
                DelClient = true,
                OperationAccount = true
            },
            new EditFieldsAccess()
            {
                FirstName = true,
                LastName = true,
                MidleName = true,
                PassortData = true,
                PhoneNumber = true
            });
    }

    public override ClientAccessInfo GetClientInfo(Client.Client client)
    {
        ClientAccessInfo clientInfo = new ClientAccessInfo(client);
        clientInfo.PassportSerie = client.SeriesAndNumberOfPassport.Serie.ToString();
        clientInfo.PassportNumber = client.SeriesAndNumberOfPassport.Number.ToString();
        return clientInfo;
    }

    public override string ToString()
    {
        return "Менеджер";
    }
}
