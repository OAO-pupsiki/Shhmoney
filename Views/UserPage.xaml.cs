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
    private void OnShowPasswordButtonClicked(object sender, EventArgs e)
    {
        passwordEntry.IsPassword = !passwordEntry.IsPassword;
    }

}