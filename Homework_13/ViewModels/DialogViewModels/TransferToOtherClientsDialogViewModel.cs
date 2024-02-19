using Bank.Application.Clients.Queries.GetClientList;
using Bank.Domain.Account;
using Homework_13.ViewModels.Base;
using MediatR;
using System.Collections.ObjectModel;
using Homework_13.Infrastructure.Commands;
using Bank.Application.Accounts.Commands.TransactionBetweenAccounts;
using System.Windows.Input;
using System.Windows;
using Homework_13.ViewModels.Helpers;

namespace Homework_13.ViewModels.DialogViewModels
{
    public class TransferToOtherClientsDialogViewModel : ViewModel
    {
        private readonly IMediator _mediator;

        private readonly TransferToOtherClientsAccountsViewModel _transferToOtherClientsAccountsViewModel;

        #region Свойства зависимости
        #region Accounts
        private ObservableCollection<Account> _accountsSelectedClient;
        public ObservableCollection<Account> AccountsSelectedClient
        {
            get => _accountsSelectedClient;
            set => Set(ref _accountsSelectedClient, value);
        }
        #endregion

        #region Amount
        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set => Set(ref _amount, value);
        }
        #endregion

        #region SelectedAccount
        private Account _selectedAccount;
        public Account SelectedAccount
        {
            get => _selectedAccount;
            set => Set(ref _selectedAccount, value);
        }
        #endregion
        #endregion

        public ClientLookUpDto SelectedClient { get; set; }
        public Account AccountFrom { get; set; }

        public TransferToOtherClientsDialogViewModel(Account accountFrom, ClientLookUpDto client,  IMediator mediator, TransferToOtherClientsAccountsViewModel viewModel)
        {
            _mediator = mediator;
            _accountsSelectedClient = new ObservableCollection<Account>(ViewModelHelper.GetAccounts(client.Id).Result.Accounts);
            _transferToOtherClientsAccountsViewModel = viewModel;
            AccountFrom = accountFrom;
            SelectedClient = client;

            #region Commands
            SaveCommand = new LambdaCommand(OnSaveCommandExecute, CanSaveCommandExecute);
            EscCommand = new LambdaCommand(OnEscCommandExecute, CanEscCommandExecute);
            #endregion
        }
        
        #region Commands
        #region SaveCommand
        public ICommand SaveCommand { get; }
        private bool CanSaveCommandExecute(object p)
        {
            return _amount > 0 && _selectedAccount != null;
        }

        private async void OnSaveCommandExecute(object p)
        {
            var command = new TransactionBetweenAccountCommand
            {
                FromAccountId = AccountFrom.Id,
                DestinationAccountId = _selectedAccount.Id,
                Amount = _amount,
            };

            var message = await _mediator.Send(command);

            MessageBox.Show(message);

            if (p is Window window)
            {
                window.Close();
            }
            _transferToOtherClientsAccountsViewModel.UpdateAccountList.Invoke();
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
}
