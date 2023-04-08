using Shhmoney.ViewModels;
namespace Shhmoney.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
		BindingContext = new SignUpViewModel();
	}
}