using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using Homework_13.Views;

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
        var mainWindow = new MainWindow
        {
            DataContext = _mainWindowViewModel
        };
        mainWindow.Show();

        if (p is Window window)
        {
            window.Close();
        }
    }
    private static bool CanSetComeInCommandExecute(object p) => true;
    #endregion

    #region OutCommand
    public ICommand OutCommand { get; }
    private static void OnOutCommandExecuted(object p)
    {
        Application.Current.Shutdown();
    }
    private static bool CanOutCommandExecute(object p) => true;
    #endregion
    #endregion    
}
