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
        public ObservableCollection<ExpenseCategory> Categories { get; set; }
        public ObservableCollection<IncomeCategory> IncomeCategories { get; set; }
        private readonly TransactionService _transactionService;
        public ObservableCollection<Account> Accounts { get; set; }
        private readonly AccountService _accountService;



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
        public void UpdateList()
        {
            if (Categories != null && IncomeCategories != null)
            {
                // Логика обновления коллекции Categories
                Categories.Clear();
                var updatedExpenseCategories = _transactionService.GetExpenseCategoriesByUser(Utils.AppContext.CurrentUser);
                foreach (var expenseCategory in updatedExpenseCategories)
                {
                    Categories.Add(expenseCategory);
                }

                // Логика обновления коллекции IncomeCategories
                IncomeCategories.Clear();
                var updatedIncomeCategories = _transactionService.GetIncomeCategoriesByUser(Utils.AppContext.CurrentUser);
                foreach (var incomeCategory in updatedIncomeCategories)
                {
                    IncomeCategories.Add(incomeCategory);
                }
            }
            if (Accounts != null)
            {
                // Логика обновления коллекции счетов
                Accounts.Clear();
                var updatedAccounts = _accountService.GetAccountsByUser(Utils.AppContext.CurrentUser);
                foreach (var account in updatedAccounts)
                {
                    Accounts.Add(account);
                }
            }
        }
    }
}
