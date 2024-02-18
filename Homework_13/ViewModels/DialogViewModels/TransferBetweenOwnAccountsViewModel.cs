using Homework_13.Infrastructure.Commands;
using MediatR;
using System.Windows.Input;
using System.Windows;
using Bank.Domain.Account;
using Homework_13.ViewModels.Base;
using Bank.Application.Accounts.Commands.TransactionBetweenAccounts;

namespace Homework_13.ViewModels.DialogViewModels
{
    public class TransferBetweenOwnAccountsViewModel : ViewModel
    {
        private IMediator _mediator;

        private BetweenOwnAccountsViewModel _viewModel;

        private Account _accountFrom;

        private Account _accountTo;

        private decimal _amount;

        public decimal Amount
        {
            get => _amount;
            set => Set(ref _amount, value);
        }

        public TransferBetweenOwnAccountsViewModel(Account accountFrom, Account accountTo, IMediator mediator, BetweenOwnAccountsViewModel viewModel)
        {
            _mediator = mediator;
            _accountFrom = accountFrom;
            _accountTo = accountTo;
            _viewModel = viewModel;

            SaveCommand = new LambdaCommand(OnSaveCommandExecute, CanSaveCommandExecute);
        }

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

            await _mediator.Send(command);

            if (p is Window window)
            {
                window.Close();
            }
            _viewModel.UpdateAccountList.Invoke();
        }
        #endregion

    }
}
