using Shhmoney.ViewModels;
namespace Shhmoney.Views;

public partial class AuthenticationPage : ContentPage
{
    private readonly AuthenticationViewModel _viewModel;

    public AuthenticationPage(AuthenticationViewModel viewModel)
	{
        _viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel; 
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel.isLoggedIn())
        {
            await Task.Delay(5);
            await Shell.Current.GoToAsync("//home/main");
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
        }
    }
}