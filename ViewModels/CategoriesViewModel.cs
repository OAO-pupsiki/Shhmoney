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
    public ICommand AddCommand { get; set; }
    private readonly TransactionService _transactionService;

    private string _name;
    public List<ExpenseCategory> Categories { get; } = new();
    public CategoriesViewModel()
    {
        _transactionService = new TransactionService();
        Categories = _transactionService.GetExpenseCategoriesByUser(Utils.AppContext.CurrentUser);
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
    public void OnProperyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}