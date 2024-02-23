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
using Bank.Domain.Worker;

namespace Homework_13.ViewModels;

public class ClientInfoViewModel : ViewModel
{
    private readonly IMediator _mediator;
    public ClientLookUpDto CurrentClient;
    private readonly MainWindowViewModel _mainWindowViewModel;

    private readonly RoleDataAccess _dataAccess;

    #region Свойствса зависимости
    #region EnableSaveClient
    private bool _enableSaveClient;
    public bool EnableSaveClient
    {
        get => _enableSaveClient;
        set => Set(ref _enableSaveClient, value);
    }

    private string _title = "Окно редактирования клиента";
    public string Title
    {
        get => _title;
        set => Set(ref _title, value);
    }
    #endregion  

    #region Firstname
    private string _firstname = null!;
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
            CheckSaveClient(_dataAccess);
        }
    }

    #endregion

    #region Lastname
    private string _lastname = null!;
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
            CheckSaveClient(_dataAccess);
        }
    }

    #endregion

    #region Patronymic
    private string _patronymic = null!;
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
            CheckSaveClient(_dataAccess);
        }
    }

    #endregion

    #region PhoneNumber
    private string _phoneNumber = null!;

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

    private bool _enableTotalIncome;
    public bool EnableTotalIncome
    {
        get => _enableTotalIncome;
        set => Set(ref _enableTotalIncome, value);
    }

    private InputValueValidationEnum _borderPhoneNumber;
    public InputValueValidationEnum BorderPhoneNumber
    {
        get => _borderPhoneNumber;
        set
        {
            Set(ref _borderPhoneNumber, value);
            CheckSaveClient(_dataAccess);
        }
    }

    #endregion

    #region PassportData
    private string _passportSeries = null!;
    public string PassportSeries
    {
        get => _passportSeries;
        set
        {
            Set(ref _passportSeries, value);
            BorderPassportSeries = InputHighlighting(_enablePassportData, Bank.Domain.Client.ValueObjects.PassportSeries.IsSeries(_passportSeries));
        }
    }

    private string _passportNumber = null!;
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

    private InputValueValidationEnum _borderPassportSeries;
    public InputValueValidationEnum BorderPassportSeries
    {
        get => _borderPassportSeries;
        set
        {
            Set(ref _borderPassportSeries, value);
            CheckSaveClient(_dataAccess);
        }
    }

    private InputValueValidationEnum _borderPassportNumber;
    public InputValueValidationEnum BorderPassportNumber
    {
        get => _borderPassportNumber;
        set
        {
            Set(ref _borderPassportNumber, value);
            CheckSaveClient(_dataAccess);
        }
    }

    #endregion

    #region TotalIncomePerMonth
    private string _totalIncomePerMonth = null!;

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
            CheckSaveClient(_dataAccess);
        }
    }
    #endregion

    #endregion      
    
    public ClientInfoViewModel(MainWindowViewModel mainWindowView, RoleDataAccess dataAccess, IMediator mediator, ClientLookUpDto client = null)
    {
        _mainWindowViewModel = mainWindowView;
        _mediator = mediator;
        this.CurrentClient = client;

        _dataAccess = dataAccess;

        FillFields(client);
        EnableFields(dataAccess);
        CheckSaveClient(dataAccess);

        OutCommand = new LambdaCommand(OnOutCommandExecute, CanOutCommandExecute);
        SaveCommand = new LambdaCommand(OnSaveCommandExecute, CanSaveCommandExecute);
    }

    /// <summary>
    /// Заполнение данных
    /// </summary>
    /// <param name="clientInfo"></param>
    private void FillFields(ClientLookUpDto clientInfo)
    {
        if (clientInfo is null)
            return;
        _firstname = clientInfo.Firstname;
        _lastname = clientInfo.Lastname;
        _patronymic = clientInfo.Patronymic;
        _phoneNumber = clientInfo.PhoneNumber;
        _passportSeries = clientInfo.PassportSeries;
        _passportNumber = clientInfo.PassportNumber;
        _totalIncomePerMonth = clientInfo.TotalIncomePerMounth;
    }

    /// <summary>
    /// Включение/отключения возможности ввода данных
    /// </summary>
    /// <param name="dataAccess"></param>
    private void EnableFields(RoleDataAccess dataAccess)
    {
        _enableFirstName = dataAccess.EditFields.FirstName;
        _enableLastName = dataAccess.EditFields.LastName;
        _enablePatronymic = dataAccess.EditFields.MiddleName;
        _enablePassportData = dataAccess.EditFields.PassportData;
        _enablePhoneNumber = dataAccess.EditFields.PhoneNumber;
        _enableTotalIncome = dataAccess.EditFields.TotalIncome;

        _borderFirstName = InputHighlighting(_enableFirstName, _firstname.Length > 0);
        _borderLastName = InputHighlighting(_enableLastName, _lastname.Length > 0);
        _borderPatronymic = InputHighlighting(_enablePatronymic, _patronymic.Length > 0);
        _borderTotalIncomePerMonth = InputHighlighting(_enableTotalIncome, true);
        _borderPhoneNumber = InputHighlighting(_enablePhoneNumber, Bank.Domain.Client.ValueObjects.PhoneNumber.IsPhoneNumber(_phoneNumber));
        if (dataAccess.EditFields.PassportData == true)
        {
            _borderPassportSeries = InputHighlighting(_enablePassportData, Bank.Domain.Client.ValueObjects.PassportSeries.IsSeries(_passportSeries));
            _borderPassportNumber = InputHighlighting(_enablePassportData, Bank.Domain.Client.ValueObjects.PassportNumber.IsNumber(_passportNumber));
        }
    }

    /// <summary>
    /// метод для блокирования кнопки сохранения, если введенные данные не валидны
    /// </summary>
    private void CheckSaveClient(RoleDataAccess dataAccess)
    {
        if (dataAccess.EditFields.PassportData == false)
        {
            EnableSaveClient = _borderFirstName != InputValueValidationEnum.Error
                               && _borderLastName != InputValueValidationEnum.Error
                               && _borderPatronymic != InputValueValidationEnum.Error
                               && _borderPhoneNumber != InputValueValidationEnum.Error;
        }
        else
        {
            EnableSaveClient = _borderFirstName != InputValueValidationEnum.Error
                               && _borderLastName != InputValueValidationEnum.Error
                               && _borderPatronymic != InputValueValidationEnum.Error
                               && _borderPassportSeries != InputValueValidationEnum.Error
                               && _borderPassportNumber != InputValueValidationEnum.Error
                               && _borderPhoneNumber != InputValueValidationEnum.Error
                               && _borderTotalIncomePerMonth != InputValueValidationEnum.Error;
        }
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
        if (CurrentClient is null) // новый клиент
        {
            var command = new CreateClientCommand
            {
                Id = Guid.NewGuid(),
                Firstname = _firstname,
                Lastname = _lastname,
                Patronymic = _patronymic,
                PhoneNumber = _phoneNumber,
                PassportSeries = _passportSeries,
                PassportNumber = _passportNumber,
                TotalIncomePerMounth = _totalIncomePerMonth
            };
            await _mediator.Send(command);
        }
        else
        {
            var command = new UpdateClientCommand
            {
                Id = CurrentClient.Id,
                Firstname = _firstname,
                Lastname = _lastname,
                Patronymic = _patronymic,
                PhoneNumber = _phoneNumber,
                PassportSeries = _passportSeries,
                PassportNumber = _passportNumber,
                TotalIncomePerMounth = Convert.ToDecimal(_totalIncomePerMonth)
            };
            await _mediator.Send(command);
        }

        if (p is Window window)
        {
            window.Close();
        }
        _mainWindowViewModel.UpdateClientList.Invoke();
    }
    #endregion
    #endregion

    
}
