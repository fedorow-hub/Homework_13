using Homework_13.Infrastructure.Commands;
using MediatR;
using System.Windows.Input;
using System.Windows;
using Bank.Domain.Account;
using Homework_13.ViewModels.Base;
using Bank.Application.Accounts.Commands.TransactionBetweenAccounts;

namespace Homework_13.ViewModels.DialogViewModels
{
    public class TransferBetweenOwnAccountsDialogViewModel : ViewModel
    {
        private readonly IMediator _mediator;

        private readonly TransferBetweenOwnAccountsViewModel _viewModel;

        private readonly Account _accountFrom;

        private readonly Account _accountTo;

        #region Свойства зависимости
        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set => Set(ref _amount, value);
        }
        #endregion

        public TransferBetweenOwnAccountsDialogViewModel(Account accountFrom, Account accountTo, IMediator mediator, TransferBetweenOwnAccountsViewModel viewModel)
        {
            _mediator = mediator;
            _accountFrom = accountFrom;
            _accountTo = accountTo;
            _viewModel = viewModel;

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
            var command = new TransactionBetweenAccountCommand
            {
                FromAccountId = _accountFrom.Id,
                DestinationAccountId = _accountTo.Id,
                Amount = _amount,
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
}
