using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shhmoney.Models;
using Shhmoney.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace Shhmoney.ViewModels
{
    public partial class TransactionViewModel : ObservableObject
    {
        private readonly UserService _userService;

        [ObservableProperty]
        bool isIncome;

        [ObservableProperty]
        Account currentAccount;

        [ObservableProperty]
        Category currentCategory;

        [ObservableProperty]
        List<Account> accounts;

        [ObservableProperty]
        List<Category> categories;

        [ObservableProperty]
        decimal value;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        DateTime date;

        [ObservableProperty]
        DateTime time;

        public TransactionViewModel(UserService userService)
        {
            _userService = userService;
            Accounts = userService.GetAccounts();
            Categories = new List<Category>();
            PropertyChanged += SetCategories;
        }

        void SetCategories(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsIncome")
                return;

            if (IsIncome)
            {
                Categories = new List<Category>(_userService.GetInocmeCategories());
            }
            else
            {
                Categories = new List<Category>(_userService.GetExpenseCategories());
            }
        }

        public void Cancel(object sender, EventArgs e)
        {
            (sender as Popup).Close();
        }

        public void Save(object sender, EventArgs e)
        {
            if (IsIncome)
            {
                (sender as Popup).Close(new Income
                {
                    Name = Name,
                    Description = Description,
                    Value = Value,
                    DateTime = DateTime.UtcNow,
                    User = Utils.AppContext.CurrentUser,
                    Account = CurrentAccount,
                    IncomeCategory = (IncomeCategory)CurrentCategory
                });
            }
            else
            {
                (sender as Popup).Close(new Expense
                {
                    Name = Name,
                    Description = Description,
                    Value = Value,
                    DateTime = DateTime.UtcNow,
                    User = Utils.AppContext.CurrentUser,
                    Account = CurrentAccount,
                    ExpenseCategory = (ExpenseCategory)CurrentCategory
                });
            }
        }
    }
}
