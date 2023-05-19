using Shhmoney.ViewModels;

namespace Shhmoney.Views;

public partial class UserPage : ContentPage
{
	public UserPage(UserViewModel userViewModel)
	{
		InitializeComponent();
        BindingContext = userViewModel;
    }

    private void GoBackButtonClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//home/main");
    }

    private bool _isPasswordVisible;
    private void OnShowPasswordButtonClicked(object sender, EventArgs e)
    {
        _isPasswordVisible = !_isPasswordVisible;
        passwordEntry.IsPassword = !_isPasswordVisible;
        confirmedPasswordEntry.IsPassword = !_isPasswordVisible;
    }

}