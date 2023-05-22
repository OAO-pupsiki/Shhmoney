using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using Shhmoney.Models;
using Shhmoney.Services;
using System.ComponentModel;
using System.Windows.Input;
using System.Xml.Linq;

namespace Shhmoney.ViewModels
{
    public partial class TransactionViewModel : ObservableObject
    {
        private readonly UserService _userService;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsExpense))]
        bool isIncome;

        public bool IsExpense => !IsIncome;

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

        public void Save(object sender, EventArgs e, Transaction transaction = null)
        {
            if (transaction != null)
            {
                if (transaction is Income)
                    _userService.RemoveIncome(transaction as Income);
                else
                    _userService.RemoveExpense(transaction as Expense);
            }
            if (IsIncome)
            {
                (sender as Popup).Close(new Income
                {
                    Name = Name,
                    Description = Description,
                    Value = Value,
                    DateTime = Date.ToUniversalTime(),
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
                    DateTime = Date.ToUniversalTime(),
                    User = Utils.AppContext.CurrentUser,
                    Account = CurrentAccount,
                    ExpenseCategory = (ExpenseCategory)CurrentCategory
                });
            }
        }
    }
}
