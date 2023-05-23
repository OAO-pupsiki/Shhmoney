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
        public ObservableCollection<Account> Accounts { get; set; }
        public ObservableCollection<MounthLimit> Limits { get; set; }


        private readonly AccountService _accountService;
        private readonly TransactionService _transactionService;
        private readonly LimitService _limitService;

        [ObservableProperty]
        Transaction currentTransaction;

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

        [ObservableProperty]
        decimal balance = 0;

        private readonly UserService _userService;

        public MainViewModel(UserService userService, LimitService limitService)
        {
            _userService = userService;
            _limitService = limitService;
            SetTransactions();
            SetBalance();
        }

        public void SetBalance()
        {
            Balance = 0;
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
            var popup = new TransactionPopup(new TransactionViewModel(_userService), null);
            var res = await Shell.Current.ShowPopupAsync(popup) as Transaction;
            if (res != null)
            {
                if (res is Income)
                {
                    _userService.AddIncome(res as Income);
                }
                else
                {
                    var categoryId = (res as Expense).ExpenseCategory.Id;              
                    if (_limitService.IsExceeded(categoryId, res.Value))
                    {
                        await Shell.Current.DisplayAlert("Ошибка", "Превышен лимит", "OK");
                        return;

                    }
                    _userService.AddExpense(res as Expense);
                }
                SetTransactions();
                SetBalance();
            }
        }

        public void RemoveTransaction()
        {
            if (CurrentTransaction is Income)
            {
                _userService.RemoveIncome(CurrentTransaction as Income);
            }
            else
            {
                _userService.RemoveExpense(CurrentTransaction as Expense);
            }
            Transactions.Remove(CurrentTransaction);
            SetBalance();
        }

        public async void ShowTransactionInfo()
        {
            var popup = new TransactionPopup(new TransactionViewModel(_userService), CurrentTransaction);
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
