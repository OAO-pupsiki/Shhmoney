using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Shhmoney.Models;
using Shhmoney.Services;
using Shhmoney.Data;

namespace Shhmoney.ViewModels
{
    public class LimitViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ExpenseCategory> Categories { get; set; }
        public ObservableCollection<Currency> Currencies { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand SelectedIndexChangedCommand { get; set; }

        private readonly LimitService _limitService;
        private readonly TransactionService _transactionService;
        private readonly CurrencyRepository _currencyRepository;
        private ExpenseCategory _selectedCategory;
        private int _limit;

        public LimitViewModel(LimitService limitService, TransactionService transactionService, CurrencyRepository currencyRepository)
        {
            _limitService = limitService;
            _transactionService = transactionService;
            _currencyRepository = currencyRepository;
            Categories = new ObservableCollection<ExpenseCategory>(_transactionService.GetExpenseCategoriesByUser(Utils.AppContext.CurrentUser));

            Currencies = new ObservableCollection<Currency>
            {
                new Currency { Code = "BYN" },
                new Currency { Code = "USD" },
                new Currency { Code = "RUB" },
                new Currency { Code = "EUR" }
            };

            AddCommand = new Command(() =>
            {
                if (SelectedCategory == null)
                {
                    Shell.Current.DisplayAlert("Ошибка", "Не выбрана категория", "ОK");
                }
                else
                {
                    _limitService.Add(SelectedCategory.Id, SelectedCurrency.Code, Limit);
                    Shell.Current.DisplayAlert("Уведомление", "Лимит успешно добавлен", "ОK");
                }
            });

            //SelectedIndexChangedCommand = new Command(OnSelectedIndexChanged);
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

                int currencyId = GetCurrencyIdByCode(SelectedCurrency?.Code);

                if (currencyId == -1)
                {
                    Shell.Current.DisplayAlert("Ошибка", "Выбрана некорректная валюта", "OK");
                    return;
                }

                MounthLimit limit = _limitService.GetMounthLimitByCategoryId(_selectedCategory.Id);

                if (limit != null)
                {
                    Limit = limit.Limit;
                    Shell.Current.DisplayAlert("Уведомление", $"Лимит для категории '{_selectedCategory.Name}' уже задан и равен {limit.Limit}", "OK");
                }
                else
                {
                    Limit = 0;
                    Shell.Current.DisplayAlert("Уведомление", $"Лимит для категории '{_selectedCategory.Name}' еще не задан", "OK");
                }

                OnPropertyChanged();
            }
        }

        private Currency _selectedCurrency;
        public Currency SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                if (_selectedCurrency == value)
                    return;
                _selectedCurrency = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int GetCurrencyIdByCode(string currencyCode)
        {
           return _currencyRepository.GetCurrencyIdByCode(currencyCode);
        }
        public void UpdateList()
        {
            Categories.Clear();
            var updatedExpenseCategories = _transactionService.GetExpenseCategoriesByUser(Utils.AppContext.CurrentUser);
            foreach (var expenseCategory in updatedExpenseCategories)
            {
                Categories.Add(expenseCategory);
            }
        }
    }
}
