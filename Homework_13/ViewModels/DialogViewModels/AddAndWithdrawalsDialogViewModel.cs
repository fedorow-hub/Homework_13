using Bank.Domain.Account;
using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.Base;
using MediatR;
using System.Windows.Input;
using Bank.Application.Accounts.Commands.AddAndWithdrawalMoney;
using System.Windows;

namespace Homework_13.ViewModels.DialogViewModels;

public class AddAndWithdrawalsDialogViewModel : ViewModel
{
    private readonly IMediator _mediator;

    private readonly AddAndWithdrawalsViewModel _viewModel;

    private readonly Account _currentAccount;

    private readonly bool _isAdd;

    #region Свойства зависимости
    private decimal _amount;
    public decimal Amount
    {
        get => _amount;
        set => Set(ref _amount, value);
    }

    private string _title;
    public string Title
    {
        get => _title;
        set => Set(ref _title, value);
    }
    #endregion

    public AddAndWithdrawalsDialogViewModel(Account account, IMediator mediator, bool isAdd, AddAndWithdrawalsViewModel viewModel)
    {
        _mediator = mediator;
        _currentAccount = account;
        _isAdd = isAdd;
        _viewModel = viewModel;
        if (!isAdd)
        {
            _amount = account.Amount;
            _title = "Снятие средств";
        }
        else
        {
            _title = "Внесение средств";
        }

        #region Commands
        SaveCommand = new LambdaCommand(OnSaveCommandExecute, CanSaveCommandExecute);
        EscCommand = new LambdaCommand(OnEscCommandExecute, CanEscCommandExecute);
        #endregion
    }

    #region Commands
    #region SaveCommand

    public ICommand SaveCommand { get; }

    private bool CanSaveCommandExecute(object p) => true;

    private async void OnSaveCommandExecute(object p)
    {
        var command = new AddAndWithdrawalMoneyCommand
        {
            Id = _currentAccount.Id,
            Amount = _amount,
            IsAdd = _isAdd
        };

        var message = await _mediator.Send(command);

        MessageBox.Show(message);

        if (p is Window window)
        {
            window.Close();
        }
        _viewModel.UpdateAccountList.Invoke();
    }
    #endregion

    #region EscCommand

    public ICommand EscCommand { get; }

    private bool CanEscCommandExecute(object p) => true;

    private void OnEscCommandExecute(object p)
    {
        if (p is Window window)
        {
            window.Close();
        }
    }
    #endregion
    #endregion

}

