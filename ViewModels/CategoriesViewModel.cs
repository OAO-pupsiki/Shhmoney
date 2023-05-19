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

        DeleteCommand = new Command<Category>(async (category) =>
        {
            if (category is ExpenseCategory expenseCategory)
            {
                bool result = await Page.DisplayAlert("Подтвердить действие", "Вы хотите удалить элемент?", "Да", "Нет");
                if (result)
                {
                    _transactionService.DeleteExpenseCategory(expenseCategory.Id, Utils.AppContext.CurrentUser);
                    Categories.Remove(expenseCategory);
                    await Page.DisplayAlert("Уведомление", "Категория успешно удалена!", "ОK");
                }
            }
            else
            {
                await Page.DisplayAlert("Ошибка", "Невозможно удалить категорию", "ОK");
            }

        });

        DeleteIncomeCategoryCommand = new Command<Category>(async (category) =>
        {
            if (category is IncomeCategory incomeCategory)
            {
                bool result = await Page.DisplayAlert("Подтвердить действие", "Вы хотите удалить элемент?", "Да", "Нет");
                if (result)
                {
                    _transactionService.DeleteIncomeCategory(incomeCategory.Id, Utils.AppContext.CurrentUser);
                    IncomeCategories.Remove(incomeCategory);
                    await Page.DisplayAlert("Уведомление", "Категория успешно удалена!", "ОK");
                }
            }
            else
            {
               await Page.DisplayAlert("Ошибка", "Невозможно удалить категорию", "ОK");
            }

        });

        ChangeCommand = new Command(async (object? category) =>
        {
            if (category is ExpenseCategory expenseCategory)
            {
                string newName = await Shell.Current.DisplayPromptAsync("Изменить имя категории", "Введите новое имя:", "Изменить", "Отмена", expenseCategory.Name);
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    _transactionService.ChangeExpenseCategory(expenseCategory.Id, newName, string.Empty);
                    await Shell.Current.DisplayAlert("Уведомление", "Наименование категории успешно изменено!", "ОK");

                    var index = Categories.IndexOf(expenseCategory);
                    Categories.RemoveAt(index);
                    expenseCategory.Name = newName;

                    Categories.Insert(index, expenseCategory);
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Ошибка", "Невозможно изменить категорию", "ОK");
            }
        });


        ChangeIncomeCategoryCommand = new Command(async (object? category) =>
        {
            if (category is IncomeCategory incomeCategory)
            {
                string newName2 = await Shell.Current.DisplayPromptAsync("Изменить имя категории", "Введите новое имя:", "Изменить", "Отмена", incomeCategory.Name);
                if (!string.IsNullOrWhiteSpace(newName2))
                {
                    _transactionService.ChangeIncomeCategory(incomeCategory.Id, newName2, string.Empty);
                    await Shell.Current.DisplayAlert("Уведомление", "Наименование категории успешно изменено!", "ОK");

                    var index = IncomeCategories.IndexOf(incomeCategory);
                    IncomeCategories.RemoveAt(index);
                    incomeCategory.Name = newName2;

                    IncomeCategories.Insert(index, incomeCategory);
                }
            }
            else
            {
                await Page.DisplayAlert("Ошибка", "Невозможно изменить категорию", "ОK");
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

    private Page _page;
    public Page Page
    {
        get => _page;
        set
        {
            if (_page == value)
                return;
            _page = value;
            OnProperyChanged();
        }
    }

    public void OnProperyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}