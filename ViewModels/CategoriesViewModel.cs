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

    public ICommand AddCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    public ICommand ChangeCommand { get; set; }
    public ICommand RefreshCommand { get; set; }

    private readonly TransactionService _transactionService;
    private string _name;
    private string _newName;
       
    public CategoriesViewModel()
    {
        _transactionService = new TransactionService();
        GetAllCategories();

        AddCommand = new Command(() =>
        {
            if (string.IsNullOrWhiteSpace(_name))
            {
                Shell.Current.DisplayAlert("Ошибка", "Наименование категории не может быть пустым", "ОK");
            }
            else 
            {
                _transactionService.AddExpenseCategory(_name, string.Empty, Utils.AppContext.CurrentUser);
                Shell.Current.DisplayAlert("Уведомление", "Успешно добавлена новая категория!", "ОK");
                Name = string.Empty;
                GetAllCategories();
            }

        });

        DeleteCommand = new Command((object? category) =>
        {
            if (category is ExpenseCategory expenseCategory)
            {
                _transactionService.DeleteExpenseCategory(expenseCategory.Id, Utils.AppContext.CurrentUser);
                Categories.Remove(expenseCategory);
                Shell.Current.DisplayAlert("Уведомление", "Категория успешно удалена!", "ОK");
                GetAllCategories();
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
                NewName = string.Empty;
                GetAllCategories();
            }
            else
            {
                Shell.Current.DisplayAlert("Ошибка", "Невозможно изменить категорию", "ОK");
            }
        });
    }

    private void GetAllCategories()
    {
        Categories = new ObservableCollection<ExpenseCategory>(_transactionService.GetExpenseCategoriesByUser(Utils.AppContext.CurrentUser));
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

    public void OnProperyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}