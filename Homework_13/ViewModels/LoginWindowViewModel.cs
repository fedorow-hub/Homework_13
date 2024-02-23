using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using Homework_13.Views;
using Bank.Application.Interfaces;
using Bank.Domain.Worker;
using MediatR;

namespace Homework_13.ViewModels;

public class LoginWindowViewModel : ViewModel
{
    private readonly IExchangeRateService _exchangeRateService;
    private readonly IMediator _mediator;
    public LoginWindowViewModel(IExchangeRateService exchangeRateService, IMediator mediator)
    {
        _exchangeRateService = exchangeRateService;
        _mediator = mediator;
        SetConsultantMode = new LambdaCommand(OnSetConsultantModeExecuted, CanSetConsultantModeExecute);
        SetManagerMode = new LambdaCommand(OnSetManagerModeExecuted, CanSetManagerModeExecute);
        OutCommand = new LambdaCommand(OnOutCommandExecuted, CanOutCommandExecute);
    }

    #region Commands

    public ICommand SetConsultantMode { get; }
    private void OnSetConsultantModeExecuted(object p)
    {
        OpenMainWindow(new Consultant(), p);

        if (p is Window window)
        {
            window.Close();
        }
    }
    private bool CanSetConsultantModeExecute(object p) => true;
    #endregion

    #region SetManagerMode
    public ICommand SetManagerMode { get; }
    private void OnSetManagerModeExecuted(object p)
    {
        OpenMainWindow(new Manager(), p);

        if (p is Window window)
        {
            window.Close();
        }
    }
    private bool CanSetManagerModeExecute(object p) => true;

    #endregion
    
    #region OutCommand
    public ICommand OutCommand { get; }
    private static void OnOutCommandExecuted(object p)
    {
        Application.Current.Shutdown();
    }
    private static bool CanOutCommandExecute(object p) => true;
    #endregion

    private void OpenMainWindow(Worker worker, object p)
    {
        var mainWindow = new MainWindow();
        var mainWindowVm = new MainWindowViewModel(worker, _exchangeRateService, _mediator);
        
        mainWindow.DataContext = mainWindowVm;
        mainWindow.Show();

        if (p is Window window)
        {
            window.Close();
        }
    }
}
