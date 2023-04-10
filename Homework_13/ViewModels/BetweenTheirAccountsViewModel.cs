
using Homework_13.Models.Bank;
using Homework_13.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Homework_13.Models.Client;

namespace Homework_13.ViewModels
{
    public class BetweenTheirAccountsViewModel : ViewModel
    {
        private Client _currentClient;
        private BankRepository _bank;
        public BetweenTheirAccountsViewModel()
        {
            
        }
        public BetweenTheirAccountsViewModel(Client CurrentClient, BankRepository bank)
        {
            _currentClient = CurrentClient;
            _bank = bank;
        }

        #region Эксперимент со свойством зависимости
        private double _FluentCount;

        public double FluentCount { get => _FluentCount; set => Set(ref _FluentCount, value); }
        #endregion

    }
}
