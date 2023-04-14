using Shhmoney.Services;
using Shhmoney.ViewModels;
namespace Shhmoney.Views;

public partial class AuthenticationPage : ContentPage
{
	public AuthenticationPage()
	{
		InitializeComponent();
		BindingContext = new AuthenticationViewModel();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

        if (IsUserAuthenticated())
        {
			await Task.Delay(100);
            await Shell.Current.GoToAsync("//home/main");
        }
    }

	private bool IsUserAuthenticated()
	{
		var authService = new AuthenticationService();
		return authService.TryAutoLogin();
    }
}