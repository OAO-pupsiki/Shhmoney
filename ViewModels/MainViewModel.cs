using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shhmoney.Models;
using Shhmoney.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using Shhmoney.Views;

namespace Shhmoney.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private ObservableCollection<Transaction> transactions;
        public ObservableCollection<Transaction> Transactions 
        { 
            get
            {
                return transactions;
            }
            set
            {
                transactions = value;
                OnPropertyChanged();
            }
        }
        public decimal Balance { get; set; } = 0;

        private readonly UserService _userService;

        public MainViewModel(UserService userService)
        {
            _userService = userService;
            SetTransactions();
            SetBalance();
        }

        public void SetBalance()
        {
            foreach(var transaction in Transactions)
            {
                if (transaction is Income)
                    Balance += transaction.Value;
                else
                    Balance -= transaction.Value;
            }
        }

        public void SetTransactions()
        {
            Transactions = new ObservableCollection<Transaction>(_userService.GetUserTransactions(Utils.AppContext.CurrentUser));
            
        }

        [RelayCommand]
        async void AddTransaction()
        {
            var popup = new TransactionPopup(new TransactionViewModel(_userService));
            var res = await Shell.Current.ShowPopupAsync(popup) as Transaction;
            if (res != null)
            {
                if (res is Income)
                {
                    _userService.AddIncome(res as Income);
                }
                else
                {
                    _userService.AddExpense(res as Expense);
                }
                SetTransactions();
                SetBalance();
            }
        }
    }
}
