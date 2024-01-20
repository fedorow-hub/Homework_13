using Microsoft.Extensions.DependencyInjection;

namespace Homework_13.ViewModels;

public class ViewModelLocator
{
    public MainWindowViewModel MainWindowModel => 
        App.Host.Services.GetRequiredService<MainWindowViewModel>();
    public LoginWindowViewModel LoginWindowModel => 
        App.Host.Services.GetRequiredService<LoginWindowViewModel>();    
}
