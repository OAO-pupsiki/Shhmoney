using Shhmoney.ViewModels;
namespace Shhmoney.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpViewModel signUpViewModel)
	{
		InitializeComponent();
		BindingContext = signUpViewModel;
	}
}