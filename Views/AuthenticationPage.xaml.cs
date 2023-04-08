using Shhmoney.ViewModels;
namespace Shhmoney.Views;

public partial class AuthenticationPage : ContentPage
{
	public AuthenticationPage()
	{
		InitializeComponent();
		BindingContext = new AuthenticationViewModel();
	}
}