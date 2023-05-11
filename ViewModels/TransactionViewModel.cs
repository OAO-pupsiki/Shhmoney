using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shhmoney.Models;
using Shhmoney.Services;

namespace Shhmoney.ViewModels
{
    public partial class TransactionViewModel : ObservableObject
    {
        private Account selectedAccount { get; set; }
        private Category SelectedCategory { get; set; }
        [ObservableProperty]
        private List<Account> accounts;
        [ObservableProperty]
        private List<IncomeCategory> incomeCategories;
        [ObservableProperty]
        private List<ExpenseCategory> expenseCategories;

        public TransactionViewModel(List<Account> accounts, List<IncomeCategory> incomeCategories, List<ExpenseCategory> expenseCategories)
        {
            this.accounts = accounts;
            this.incomeCategories = incomeCategories;
            this.expenseCategories = expenseCategories;
        }

        [RelayCommand]
        public void SelectedAccount(object sender)
        {
            selectedAccount = (sender as Picker).SelectedItem as Account;
        }
    }
}
