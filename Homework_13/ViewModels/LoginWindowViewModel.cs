using Homework_13.Infrastructure.Commands;
using Homework_13.Models.Bank;
using Homework_13.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace Homework_13.ViewModels;

public class LoginWindowViewModel : ViewModel
{
    private readonly MainWindowViewModel _mainWindowViewModel;

    public LoginWindowViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        ComeInCommand = new LambdaCommand(OnComeInCommandExecuted, CanSetComeInCommandExecute);
        OutCommand = new LambdaCommand(OnOutCommandExecuted, CanOutCommandExecute);
    }

    #region Commands

    #region ComeInCommand
    public ICommand ComeInCommand { get; }
    private void OnComeInCommandExecuted(object p)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.DataContext = _mainWindowViewModel;
        mainWindow.Show();

        if (p is Window window)
        {
            window.Close();
        }
    }
    private bool CanSetComeInCommandExecute(object p) => true;
    #endregion

    #region OutCommand
    public ICommand OutCommand { get; }
    private void OnOutCommandExecuted(object p)
    {
        Application.Current.Shutdown();
    }
    private bool CanOutCommandExecute(object p) => true;
    #endregion
    #endregion    
}
