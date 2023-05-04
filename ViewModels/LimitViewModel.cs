using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shhmoney.Models;
using Shhmoney.Services;

namespace Shhmoney.ViewModels
{
    class LimitViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ExpenseCategory> Categories { get; set; }

        private readonly LimitService _limitService;
        private readonly TransactionService _transactionService;
        private ExpenseCategory _selectedCategory;
        private int _limit;

        public LimitViewModel()
        {
            _limitService = new LimitService();
            _transactionService = new TransactionService();
            Categories = new ObservableCollection<ExpenseCategory>(_transactionService.GetExpenseCategoriesByUser(Utils.AppContext.CurrentUser));

            AddCommand = new Command(() =>
            {
                if (SelectedCategory == null)
                {
                    Shell.Current.DisplayAlert("Ошибка", "Не выбрана категория", "ОK");
                }
                else
                {
                    _limitService.Add(SelectedCategory.Id, "BYN", Limit);
                    Shell.Current.DisplayAlert("Уведомление", "Лимит успешно добавлен", "ОK");
                    SelectedCategory = null;
                    Limit = 0;
                }
            });
        }

        public ICommand AddCommand { get; }

        public ExpenseCategory SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory == value)
                    return;
                _selectedCategory = value;
                OnProperyChanged();
            }
        }

        public int Limit
        {
            get => _limit;
            set
            {
                if (_limit == value)
                    return;
                _limit = value;
                OnProperyChanged();
            }
        }

        public void OnProperyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

