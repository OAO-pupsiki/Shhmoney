using System.Collections.Generic;
using Shhmoney.Models;
using Shhmoney.Services;
using Shhmoney.ViewModels;
namespace Shhmoney.Views;

public partial class LimitsPage : ContentPage
{
    public LimitViewModel _limitViewModel { get; set; }
    public LimitService _limitService { get; set; }
    public LimitsPage(LimitViewModel limitViewModel, LimitService limitService)
    {
        InitializeComponent();
        BindingContext = limitViewModel;
        _limitViewModel = limitViewModel;
        CurrencyTypes.SelectedIndex = 0;
        _limitService = limitService;
    }
    void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        //DisplayAlert("Вид денежных средств", $"Вы выбрали: {MoneyTypes.SelectedItem}", "Oк");
    }
    private void OnMainClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/main");
    }
    private void OnCategoriesClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/categories");
    }
    private void OnAccountsClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/accounts");
    }
    private void OnArticlesClicked(object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//home/articles");
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _limitViewModel.UpdateList();
    }
    private void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        string selectedCurrencyCode = picker.SelectedItem as string;
        _limitViewModel.SelectedCurrency = _limitViewModel.Currencies.FirstOrDefault(c => c.Code == selectedCurrencyCode);

        if (_limitViewModel.SelectedCategory == null)
        {
            //Shell.Current.DisplayAlert("Ошибка", "Не выбрана категория", "OK");
            return;
        }
        if (_limitViewModel.SelectedCurrency == null)
        {
            Shell.Current.DisplayAlert("Ошибка", "Не выбрана валюта", "ОK");
            return;
        }

        int currencyId = _limitViewModel.GetCurrencyIdByCode(_limitViewModel.SelectedCurrency.Code);

        if (currencyId == -1)
        {
            Shell.Current.DisplayAlert("Ошибка", "Выбрана некорректная валюта", "OK");
            return;
        }

        MounthLimit limit = _limitService.GetMounthLimitByCategoryId(_limitViewModel.SelectedCategory.Id);

        if (limit != null)
        {
            _limitViewModel.Limit = limit.Limit;
            Shell.Current.DisplayAlert("Уведомление", $"Лимит для категории '{_limitViewModel.SelectedCategory.Name}' уже задан и равен {limit.Limit} BYN", "OK");
        }
        else
        {
            _limitViewModel.Limit = 0;
            Shell.Current.DisplayAlert("Уведомление", $"Лимит для категории '{_limitViewModel.SelectedCategory.Name}' еще не задан", "OK");
        }
    }

}
