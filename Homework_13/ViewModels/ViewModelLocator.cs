using Microsoft.Extensions.DependencyInjection;

namespace Homework_13.ViewModels;

public class ViewModelLocator
{
    public MainWindowViewModel MainWindowModel => 
        App.Host.Services.GetRequiredService<MainWindowViewModel>();
    public LoginWindowViewModel LoginWindowModel => 
        App.Host.Services.GetRequiredService<LoginWindowViewModel>();
    public ClientInfoViewModel ClientInfoModel =>
        App.Host.Services.GetRequiredService<ClientInfoViewModel>();
    public OperationsWindowViewModel OperationWindowModel =>
        App.Host.Services.GetRequiredService<OperationsWindowViewModel>();
    public OpenAccountViewModel OpenAccountModel =>
        App.Host.Services.GetRequiredService<OpenAccountViewModel>();
    public AccountInfoViewModel AccountViewModel =>
        App.Host.Services.GetRequiredService<AccountInfoViewModel>();
    public AddAndWithdrawalsViewModel AddAndWithdrawalsViewModel =>
        App.Host.Services.GetRequiredService<AddAndWithdrawalsViewModel>();
    public DialogViewModel DialogModel =>
        App.Host.Services.GetRequiredService<DialogViewModel>();
}
