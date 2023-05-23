using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shhmoney.Services;

namespace Shhmoney.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        private readonly AuthenticationService _authenticationService;

        public LoginViewModel(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        bool rememberMe;

        [RelayCommand]
        void SignUpPage()
        {
            Shell.Current.GoToAsync("//auth/signup");
        }

        [RelayCommand]
        void Login()
        {
            try
            {
                _authenticationService.Login(Username, Password, RememberMe);
                Shell.Current.GoToAsync("//home/main");
            }
            catch (Exception e)
            {
                Shell.Current.DisplayAlert("Error", e.Message, "Ok");
            }
        }
    }
}
