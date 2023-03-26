using Homework_13.Infrastructure.Commands;
using Homework_13.Models.Worker;
using Homework_13.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace Homework_13.ViewModels;

public class LoginWindowViewModel : ViewModel
{
    public LoginWindowViewModel()
    {
        SetConsultantMode = new LambdaCommand(OnSetConsultantModeExecuted, CanSetConsultantModeExecute);
        SetManagerMode = new LambdaCommand(OnSetManagerModeExecuted, CanSetManagerModeExecute);
        OutCommand = new LambdaCommand(OnOutCommandExecuted, CanOutCommandExecute);
    }

    #region Commands

    #region SetWorkerMode
    public ICommand SetConsultantMode { get; }
    private void OnSetConsultantModeExecuted(object p)
    {
        OpenMainWindow(new Consultant(), p);
    }
    private bool CanSetConsultantModeExecute(object p) => true;
    #endregion

    #region SetManagerMode
    public ICommand SetManagerMode { get; }
    private void OnSetManagerModeExecuted(object p)
    {
        OpenMainWindow(new Manager(), p);
    }
    private bool CanSetManagerModeExecute(object p) => true;

    #endregion

    #region OutCommand
    public ICommand OutCommand { get; }
    private void OnOutCommandExecuted(object p)
    {
        Application.Current.Shutdown();
    }
    private bool CanOutCommandExecute(object p) => true;
    #endregion

    private void OpenMainWindow(Worker worker, object p)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.DataContext = new MainWindowViewModel(worker);
        mainWindow.Show();

        if (p is Window window)
        {
            window.Close();
        }
    }

    #endregion
}
