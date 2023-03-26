using Microsoft.Extensions.DependencyInjection;

namespace Homework_13.ViewModels;

public class ViewModelLocator
{
    public MainWindowViewModel MainWindowViewModel => 
        App.Host.Services.GetRequiredService<MainWindowViewModel>();
    public LoginWindowViewModel LoginWindowViewModel => 
        App.Host.Services.GetRequiredService<LoginWindowViewModel>();
}
