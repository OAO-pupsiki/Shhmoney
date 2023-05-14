using Shhmoney.Models;
using Shhmoney.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Shhmoney.ViewModels;

public class CategoriesViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public ObservableCollection<ExpenseCategory> Categories { get; set; } 
    public ObservableCollection<IncomeCategory> IncomeCategories { get; set; } 

    public ICommand AddCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    public ICommand DeleteIncomeCategoryCommand { get; set; }
    public ICommand ChangeCommand { get; set; }
    public ICommand ChangeIncomeCategoryCommand { get; set; }
    public ICommand RefreshCommand { get; set; }

    private readonly TransactionService _transactionService;
    private string _name;
    private string _newName;
       
    public CategoriesViewModel(TransactionService transactionService)
    {
        _transactionService = transactionService;
        Categories = new ObservableCollection<ExpenseCategory>(_transactionService.GetExpenseCategoriesByUser(Utils.AppContext.CurrentUser));
        IncomeCategories = new ObservableCollection<IncomeCategory>(_transactionService.GetIncomeCategoriesByUser(Utils.AppContext.CurrentUser));

        AddCommand = new Command(() =>
        {
            if (string.IsNullOrWhiteSpace(_name))
            {
                Shell.Current.DisplayAlert("Ошибка", "Наименование категории не может быть пустым", "ОK");
            }
            else 
            {
                if (IsExpenses)
                {
                    Categories.Add(_transactionService.AddExpenseCategory(_name, string.Empty, Utils.AppContext.CurrentUser));
                    Shell.Current.DisplayAlert("Уведомление", "Успешно добавлена новая категория!", "ОK");
                }
                else
                {
                    IncomeCategories.Add(_transactionService.AddIncomeCategory(_name, string.Empty, Utils.AppContext.CurrentUser));
                    Shell.Current.DisplayAlert("Уведомление", "Успешно добавлена новая категория!", "ОK");
                }
                Name = string.Empty;
            }

        });

        void Delete(ExpenseCategory expenseCategory)
        {
            if (Categories.Contains(expenseCategory))
               {
                Categories.Remove(expenseCategory);
            }
        }

        DeleteCommand = new Command((object? category) =>
        {
            if (category is ExpenseCategory expenseCategory)
            {
                _transactionService.DeleteExpenseCategory(expenseCategory.Id, Utils.AppContext.CurrentUser);
                Categories.Remove(expenseCategory);
                Shell.Current.DisplayAlert("Уведомление", "Категория успешно удалена!", "ОK");
            }
            else
            {
                Shell.Current.DisplayAlert("Ошибка", "Невозможно удалить категорию", "ОK");
            }

        });

        DeleteIncomeCategoryCommand = new Command((object? category) =>
        {
            if (category is IncomeCategory incomeCategory)
            {
                _transactionService.DeleteIncomeCategory(incomeCategory.Id, Utils.AppContext.CurrentUser);
                IncomeCategories.Remove(incomeCategory);
                Shell.Current.DisplayAlert("Уведомление", "Категория успешно удалена!", "ОK");
            }
            else
            {
                Shell.Current.DisplayAlert("Ошибка", "Невозможно удалить категорию", "ОK");
            }

        });

        ChangeCommand = new Command((object? category) =>
        {
            //var test = Shell.Current.GetValuenewName;
            if (category is ExpenseCategory expenseCategory)
            {             
               _transactionService.ChangeExpenseCategory(expenseCategory.Id, NewName, string.Empty);
                Shell.Current.DisplayAlert("Уведомление", "Наименование категории успешно изменено!", "ОK");

                var index = Categories.IndexOf(expenseCategory);
                Categories.RemoveAt(index);
                expenseCategory.Name = NewName;

                Categories.Insert(index, expenseCategory);

                NewName = string.Empty;
            }
            else
            {
                Shell.Current.DisplayAlert("Ошибка", "Невозможно изменить категорию", "ОK");
            }
        });

        ChangeIncomeCategoryCommand = new Command((object? category) =>
        {
            //var test = Shell.Current.GetValuenewName;
            if (category is IncomeCategory incomeCategory)
            {
                _transactionService.ChangeIncomeCategory(incomeCategory.Id, NewName2, string.Empty);
                Shell.Current.DisplayAlert("Уведомление", "Наименование категории успешно изменено!", "ОK");

                var index = IncomeCategories.IndexOf(incomeCategory);
                IncomeCategories.RemoveAt(index);
                incomeCategory.Name = NewName2;

                IncomeCategories.Insert(index, incomeCategory);

                NewName2 = string.Empty;
            }
            else
            {
                Shell.Current.DisplayAlert("Ошибка", "Невозможно изменить категорию", "ОK");
            }
        });
    }

   
    [Required(ErrorMessage = "Пожалуйста введите наименование новой категории.")]
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

    public string NewName
    {
        get => _newName;
        set
        {
            if (_newName == value)
                return;
            _newName = value;
            OnProperyChanged();
        }
    }

    private string _newName2;
    public string NewName2
    {
        get => _newName2;
        set
        {
            if (_newName2 == value)
                return;
            _newName2 = value;
            OnProperyChanged();
        }
    }

    private bool _isExpenses = true;
    public bool IsExpenses
    {
        get => _isExpenses;
        set
        {
            if (_isExpenses == value)
                return;
            _isExpenses = value;
            OnProperyChanged();
        }
    }

    public void OnProperyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}