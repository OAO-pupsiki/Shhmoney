using Shhmoney.Services;
using Shhmoney.Utils;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using Shhmoney.Views;
using Shhmoney.ViewModels;
using System.Windows.Input;

namespace Shhmoney;

public partial class AppShell : Shell
{ 
    public AuthenticationService _authenticationService { get; set; }
	public AppShell(CurrencyExchangeRate currencyExchangeRate, AuthenticationService authenticationService)
	{
		InitializeComponent();
        currencyExchangeRate.LoadCurrencies();
        _authenticationService = authenticationService;

    }
    public ICommand TapCommand => new Command<string>((url) => DisplayAlert("Уведомление", "Наименование категории успешно изменено!", "ОK"));

    private void OnSupportButtonClicked(object sender, EventArgs e)
    {
        DisplayAlert("Контактные данные", "shhmoneyhelp@gmail.com", "ОK");

    }
    private void OnUserButtonClicked(object sender, EventArgs e)
    {
        GoToAsync("//home/users");
    }

    private async void OnExitButtonClicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Подтвердить действие", "Вы хотите выйти?", "Да", "Нет");

        if (result)
        {
           _authenticationService.LogOut();
           await GoToAsync("//auth_main");
        }
    }

}