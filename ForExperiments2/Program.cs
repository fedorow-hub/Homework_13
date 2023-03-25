using BankDAL.DataOperations;
using BankDAL.Models;
using System.Configuration;

//var setting = new ConnectionStringSettings
//{
//    Name = "MyConnectionString",
//    ConnectionString = @"Data Source;"
//};

//Configuration config;
//config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
//config.ConnectionStrings.ConnectionStrings.Add(setting);
//config.Save();

//ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;

//if (section.SectionInformation.IsProtected)
//{
//    section.SectionInformation.UnprotectSection();
//}
//else
//{
//    section.SectionInformation.ProtectSection(
//        "DataProtectionConfigurationProvider");
//}

//config.Save();

//Console.WriteLine($"Protected={section.SectionInformation.IsProtected}");

//Console.WriteLine(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

//Console.ReadLine();

ClientDAL clientDAL = new ClientDAL();

List<ClientViewModel> clientModelViews;
clientModelViews = clientDAL.GetAllClients();
//Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EvelDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
Console.WriteLine();

