﻿using System;
using Bank.Application.Clients.Queries.GetClientList;
using Bank.Domain.Account;
using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.Base;
using MediatR;
using System.Windows.Input;
using Bank.Application.Accounts.Commands.CreateAccount;
using System.Windows;

namespace Homework_13.ViewModels;

public class AccountInfoViewModel : ViewModel
{
    private readonly ClientLookUpDto _currentClient;
    private readonly IMediator _mediator;
    private readonly OpenAccountViewModel _openAccountViewModel;

    #region Свойства зависимости
    private TypeOfAccount[] _accountTypes = { TypeOfAccount.Plain, TypeOfAccount.Credit, TypeOfAccount.Deposit };
    public TypeOfAccount[] AccountTypes
    {
        get => _accountTypes;
        set => Set(ref _accountTypes, value);
    }

    private string _type = "Рассчетный";
    public string Type
    {
        get => _type;
        set => Set(ref _type, value);
    }

    private decimal _amount;
    public decimal Amount
    {
        get => _amount;
        set => Set(ref _amount, value);
    }

    private byte _accountTerm = byte.MaxValue;
    public byte AccountTerm
    {
        get => _accountTerm;
        set => Set(ref _accountTerm, value);
    }
    #endregion

    public AccountInfoViewModel(ClientLookUpDto client, IMediator mediator, OpenAccountViewModel openAccountViewModel)
    {
        _currentClient = client;
        _mediator = mediator;
        _openAccountViewModel = openAccountViewModel;

        #region Commands
        SaveCommand = new LambdaCommand(OnSaveCommandExecute, CanSaveCommandExecute);
        OutCommand = new LambdaCommand(OnOutCommandExecute, CanOutCommandExecute);
        #endregion
    }

    #region Commands
    #region OutCommand
    public ICommand OutCommand { get; }
    private bool CanOutCommandExecute(object p) => true;
    private void OnOutCommandExecute(object p)
    {
        if (p is Window window)
        {
            window.Close();
        }
    }
    #endregion

    #region SaveCommand
    public ICommand SaveCommand { get; }
    private bool CanSaveCommandExecute(object p) => true;
    private async void OnSaveCommandExecute(object p)
    {
        var command = new CreateAccountCommand
        {
            ClientId = _currentClient.Id,
            CreatedAt = DateTime.Now,
            AccountTerm = AccountTerm,
            Amount = Convert.ToDecimal(_amount),
            TypeOfAccount = TypeOfAccount.Parse(_type)
        };

        var message = await _mediator.Send(command);

        MessageBox.Show(message);

        if (p is Window window)
        {
            window.Close();
        }
        _openAccountViewModel.UpdateAccountList.Invoke();
    }
    #endregion
    #endregion
}

