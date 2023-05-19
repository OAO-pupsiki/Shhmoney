using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shhmoney.Models;
using Shhmoney.Services;

namespace Shhmoney.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Account> Accounts { get; set; }
        public ObservableCollection<Currency> Currencies { get; set; }

        public ICommand AddAccountCommand { get; set; }

        private readonly AccountService _accountService;

        //private readonly CurrencyService _currencyService;
        private string _name;
        private PaymentType _paymentType;
       // private Currency _selectedCurrency;

        public AccountViewModel(AccountService accountService)
        {
            _accountService = accountService;
            Accounts = new ObservableCollection<Account>();
            //_currencyService = new CurrencyService();
            // Currencies = new ObservableCollection<Currency>(_currencyService.GetAllCurrencies());

            AddAccountCommand = new Command(() =>
            {
                var account = new Account
                {
                    Name = Name,
                    PaymentType = PaymentType,
                    UserId = Utils.AppContext.CurrentUser.Id,
                    //CurrencyId = SelectedCurrency.Id,
                    CurrencyId = 1,
                };
                if (IsCard)
                {
                    account.PaymentType = PaymentType.Card;
                    _accountService.AddAccount(account);
                    Shell.Current.DisplayAlert("Уведомление", "Платежная карта успешно добавлена", "ОK");
                }
                else 
                {
                    account.PaymentType = PaymentType.Cash;
                    _accountService.AddAccount(account);
                    Shell.Current.DisplayAlert("Уведомление", "Счет успешно добавлен", "ОK");
                }
                Name = null;
                PaymentType = default(PaymentType);
                //SelectedCurrency = null;
            });
        }      

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnProperyChanged();
            }
        }

        private bool _isCard = true;
        public bool IsCard
        {
            get => _isCard;
            set
            {
                if (_isCard == value)
                    return;
                _isCard = value;
                OnProperyChanged();
            }
        }

        public PaymentType PaymentType
        {
            get => _paymentType;
            set
            {
                if (_paymentType == value)
                    return;
                _paymentType = value;
                OnProperyChanged();
            }
        }

       /* public Currency SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                if (_selectedCurrency == value)
                    return;
                _selectedCurrency = value;
                OnProperyChanged();
            }
        }*/

        public void OnProperyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
