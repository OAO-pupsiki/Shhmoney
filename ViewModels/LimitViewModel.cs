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
using Shhmoney.Data;
using System.Security.Cryptography.X509Certificates;

namespace Shhmoney.ViewModels
{
    class LimitViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ExpenseCategory> Categories { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand SelectedIndexChangedCommand { get; set; }

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
                }
            });
        }

        public void SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedCategory == null)
            {
                Shell.Current.DisplayAlert("Ошибка", "Не выбрана категория", "OK");
                return;
            }

            MounthLimit limit = _limitService.GetMounthLimitById(SelectedCategory.Id);

            if (limit != null)
            {
                Limit = limit.Limit;
                Shell.Current.DisplayAlert("Уведомление", $"Лимит для категории '{SelectedCategory.Name}' уже задан и равен {limit.Limit} BYN", "OK");
            }
            else
            {
                Limit = 0;
                Shell.Current.DisplayAlert("Уведомление", $"Лимит для категории '{SelectedCategory.Name}' еще не задан", "OK");
            }
        }
        public ExpenseCategory SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory == value)
                    return;
                _selectedCategory = value;

                if (_selectedCategory == null)
                {
                    Shell.Current.DisplayAlert("Ошибка", "Не выбрана категория", "OK");
                    return;
                }

                MounthLimit limit = _limitService.GetMounthLimitById(_selectedCategory.Id);

                if (limit != null)
                {
                    Limit = limit.Limit;
                    Shell.Current.DisplayAlert("Уведомление", $"Лимит для категории '{_selectedCategory.Name}' уже задан и равен {limit.Limit} BYN", "OK");
                }
                else
                {
                    Limit = 0;
                    Shell.Current.DisplayAlert("Уведомление", $"Лимит для категории '{_selectedCategory.Name}' еще не задан", "OK");
                }

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

