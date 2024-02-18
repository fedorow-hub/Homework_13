using Bank.Domain.Account;
using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.Base;
using MediatR;
using System.Windows.Input;
using Bank.Application.Accounts.Commands.AddAndWithdrawalMoney;
using System.Windows;

namespace Homework_13.ViewModels;

public class DialogViewModel : ViewModel
{
    private IMediator _mediator;

    private AddAndWithdrawalsViewModel _addAndWithdrawalsViewModel;

    private Account _currentAccount;

    private bool _isAdd;

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

    public DialogViewModel(Account account, IMediator mediator, bool isAdd, AddAndWithdrawalsViewModel addAndWithdrawalsViewModel)
    {
        _mediator = mediator;
        _currentAccount = account;
        _isAdd = isAdd;
        if (!isAdd)
        {
            _amount = account.Amount;
            _title = "Снятие средств";
        }
        else
        {
            _title = "Внесение средств";
        }
        _addAndWithdrawalsViewModel = addAndWithdrawalsViewModel;

        SaveCommand = new LambdaCommand(OnSaveCommandExecute, CanSaveCommandExecute);
    }

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

        await _mediator.Send(command);

        if (p is Window window)
        {
            window.Close();
        }
        _addAndWithdrawalsViewModel.UpdateAccountList.Invoke();
    }
    #endregion


}

