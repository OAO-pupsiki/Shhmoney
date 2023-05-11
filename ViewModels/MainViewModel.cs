using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shhmoney.Models;
using Shhmoney.Services;
using System.Collections.ObjectModel;
using Shhmoney.Views;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Maui.Controls.Platform;

namespace Shhmoney.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<Transaction> Transactions { get; set; }
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
            
            //Shell.Current.Navigation.PushModalAsync(new TransactionPage(new TransactionViewModel(_userService.GetAccounts(), _userService.GetInocmeCategories(), _userService.GetExpenseCategories())));
        }
    }
}
