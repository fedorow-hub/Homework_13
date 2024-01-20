using Bank.Application.Clients.Commands.CreateClient;
using Bank.Application.Clients.Commands.UpdateClient;
using Bank.Domain.Client.ValueObjects;
using Homework_13.Infrastructure;
using Homework_13.Infrastructure.Commands;
using Homework_13.ViewModels.Base;
using MediatR;
using System;
using System.Windows;
using System.Windows.Input;
using Bank.Application.Clients.Queries.GetClientList;

namespace Homework_13.ViewModels;

public class ClientInfoViewModel : ViewModel
{
    private IMediator _mediator;
    public ClientLookUpDTO currentClient;

    public ClientInfoViewModel() { }

    public ClientInfoViewModel(ClientLookUpDTO client, IMediator mediator)
    {
        _mediator = mediator;
        this.currentClient = client;

        FillFields(client);
        CheckSaveClient();

        OutCommand = new LambdaCommand(OnOutCommandExecute, CanOutCommandExecute);
        SaveCommand = new LambdaCommand(OnSaveCommandExecute, CanSaveCommandExecute);
    }

    /// <summary>
    /// Заполнение данных
    /// </summary>
    /// <param name="clientInfo"></param>
    private void FillFields(ClientLookUpDTO clientInfo)
    {
        if (clientInfo is null)
            return;
        _firstname = clientInfo.Firstname ?? String.Empty;
        _lastname = clientInfo.Lastname ?? String.Empty;
        _patronymic = clientInfo.Patronymic ?? String.Empty;
        _phoneNumber = clientInfo.PhoneNumber ?? String.Empty;
        _passportSerie = clientInfo.PassportSerie;
        _passportNumber = clientInfo.PassportNumber ?? String.Empty;
        _totalIncomePerMonth = clientInfo.TotalIncomePerMounth ?? String.Empty;
    }


    /// <summary>
    /// метод для блокирования кнопки сохранения, если введенные данные не валидны
    /// </summary>
    /// <param name="dataAccess"></param>
    private void CheckSaveClient()
    {
        EnableSaveClient = _borderFirstName != InputValueValidationEnum.Error
                        && _firstname != ""
                        && _borderLastName != InputValueValidationEnum.Error
                        && _lastname != ""
                        && _borderPatronymic != InputValueValidationEnum.Error
                        && _patronymic != ""
                        && _borderPassportSerie != InputValueValidationEnum.Error
                        && _passportSerie != ""
                        && _borderPassportNumber != InputValueValidationEnum.Error
                        && _passportNumber != ""
                        && _borderPhoneNumber != InputValueValidationEnum.Error
                        && _phoneNumber != ""
                        && _borderTotalIncomePerMonth != InputValueValidationEnum.Error
                        && _totalIncomePerMonth != "";
    }

    /// <summary>
    /// метод установки модификаторов валидности
    /// </summary>
    /// <param name="isEnable"></param>
    /// <param name="isValid"></param>
    /// <returns></returns>
    private InputValueValidationEnum InputHighlighting(bool isEnable, bool isValid)
    {
        if (!isValid) return InputValueValidationEnum.Error;
        if (!isEnable) return InputValueValidationEnum.Disable;

        return InputValueValidationEnum.Default;
    }

    #region ClientInfo

    #region Firstname
    private string _firstname;
    public string Firstname
    {
        get => _firstname;
        set
        {
            Set(ref _firstname, value);
            BorderFirstName = InputHighlighting(_enableFirstName, Bank.Domain.Client.ValueObjects.Firstname.IsName(_firstname));
        }
    }

    private bool _enableFirstName;
    public bool EnableFirstName
    {
        get => _enableFirstName;
        set => Set(ref _enableFirstName, value);
    }

    private InputValueValidationEnum _borderFirstName;
    public InputValueValidationEnum BorderFirstName
    {
        get => _borderFirstName;
        set
        {
            Set(ref _borderFirstName, value);
            CheckSaveClient();
        }
    }

    #endregion

    #region Lastname
    private string _lastname;
    public string Lastname
    {
        get => _lastname;
        set
        {
            Set(ref _lastname, value);
            BorderLastName = InputHighlighting(_enableLastName, Bank.Domain.Client.ValueObjects.Lastname.IsName(_lastname));
        }
    }

    private bool _enableLastName;
    public bool EnableLastName
    {
        get => _enableLastName;
        set => Set(ref _enableLastName, value);
    }

    private InputValueValidationEnum _borderLastName;
    public InputValueValidationEnum BorderLastName
    {
        get => _borderLastName;
        set
        {
            Set(ref _borderLastName, value);
            CheckSaveClient();
        }
    }

    #endregion

    #region Patronymic
    private string _patronymic;
    public string Patronymic
    {
        get => _patronymic;
        set
        {
            Set(ref _patronymic, value);
            BorderPatronymic = InputHighlighting(_enablePatronymic, Bank.Domain.Client.ValueObjects.Patronymic.IsName(_patronymic));
        }
    }

    private bool _enablePatronymic;
    public bool EnablePatronymic
    {
        get => _enablePatronymic;
        set => Set(ref _enablePatronymic, value);
    }

    private InputValueValidationEnum _borderPatronymic;
    public InputValueValidationEnum BorderPatronymic
    {
        get => _borderPatronymic;
        set
        {
            Set(ref _borderPatronymic, value);
            CheckSaveClient();
        }
    }

    #endregion

    #region PhoneNumber
    private string _phoneNumber;

    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            Set(ref _phoneNumber, value);
            BorderPhoneNumber = InputHighlighting(_enablePhoneNumber, Bank.Domain.Client.ValueObjects.PhoneNumber.IsPhoneNumber(_phoneNumber));
        }
    }

    private bool _enablePhoneNumber;
    public bool EnablePhoneNumber
    {
        get => _enablePhoneNumber;
        set => Set(ref _enablePhoneNumber, value);
    }

    private InputValueValidationEnum _borderPhoneNumber;
    public InputValueValidationEnum BorderPhoneNumber
    {
        get => _borderPhoneNumber;
        set
        {
            Set(ref _borderPhoneNumber, value);
            CheckSaveClient();
        }
    }

    #endregion

    #region PassportData
    private string _passportSerie;
    public string PassportSerie
    {
        get => _passportSerie;
        set
        {
            Set(ref _passportSerie, value);
            BorderPassportSerie = InputHighlighting(_enablePassportData, Bank.Domain.Client.ValueObjects.PassportSerie.IsSeries(_passportSerie));
        }
    }

    private string _passportNumber;
    public string PassportNumber
    {
        get => _passportNumber;
        set
        {
            Set(ref _passportNumber, value);
            BorderPassportNumber = InputHighlighting(_enablePassportData, Bank.Domain.Client.ValueObjects.PassportNumber.IsNumber(_passportNumber));
        }
    }

    private bool _enablePassportData;
    public bool EnablePassportData
    {
        get => _enablePassportData;
        set => Set(ref _enablePassportData, value);
    }

    private InputValueValidationEnum _borderPassportSerie;
    public InputValueValidationEnum BorderPassportSerie
    {
        get => _borderPassportSerie;
        set
        {
            Set(ref _borderPassportSerie, value);
            CheckSaveClient();
        }
    }

    private InputValueValidationEnum _borderPassportNumber;
    public InputValueValidationEnum BorderPassportNumber
    {
        get => _borderPassportNumber;
        set
        {
            Set(ref _borderPassportNumber, value);
            CheckSaveClient();
        }
    }

    #endregion

    #region TotalIncomePerMonth
    private string _totalIncomePerMonth;

    public string TotalIncomePerMonth
    {
        get => _totalIncomePerMonth;
        set
        {
            Set(ref _totalIncomePerMonth, value);
            BorderTotalIncomePerMonth = InputHighlighting(_enableTotalIncomePerMonth, TotalIncomePerMounth.IsIncome(_totalIncomePerMonth));
        }
    }

    private bool _enableTotalIncomePerMonth;
    public bool EnableTotalIncomePerMonth
    {
        get => _enablePhoneNumber;
        set => Set(ref _enablePhoneNumber, value);
    }

    private InputValueValidationEnum _borderTotalIncomePerMonth;
    public InputValueValidationEnum BorderTotalIncomePerMonth
    {
        get => _borderTotalIncomePerMonth;
        set
        {
            Set(ref _borderTotalIncomePerMonth, value);
            CheckSaveClient();
        }
    }

    #endregion

    #endregion      

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
        if (currentClient.Id == Guid.Empty) // новый клиент
        {
            var command = new CreateClientCommand
            {
                Id = Guid.NewGuid(),
                Firstname = _firstname,
                Lastname = _lastname,
                Patronymic = _patronymic,
                PhoneNumber = _phoneNumber,
                PassportSerie = _passportSerie,
                PassportNumber = _passportNumber,
                TotalIncomePerMounth = _totalIncomePerMonth
            };
            await _mediator.Send(command);
        }
        else
        {
            var command = new UpdateClientCommand
            {
                Id = currentClient.Id,
                Firstname = _firstname,
                Lastname = _lastname,
                Patronymic = _patronymic,
                PhoneNumber = _phoneNumber,
                PassportSerie = _passportSerie,
                PassportNumber = _passportNumber,
                TotalIncomePerMounth = Convert.ToDecimal(_totalIncomePerMonth)
            };
            await _mediator.Send(command);
        }

        if (p is Window window)
        {
            window.Close();
        }
    }
    #endregion
    #endregion

    #region EnableSaveClient
    private bool _enableSaveClient;
    public bool EnableSaveClient
    {
        get => _enableSaveClient;
        set => Set(ref _enableSaveClient, value);
    }
    #endregion  
}
