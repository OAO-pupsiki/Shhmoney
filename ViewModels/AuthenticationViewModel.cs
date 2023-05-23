using Shhmoney.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Shhmoney.ViewModels
{
    public partial class AuthenticationViewModel : ObservableObject
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationViewModel(AuthenticationService authenticationService) 
        {
            _authenticationService = authenticationService;
        }

        [RelayCommand]
        void LoginPage() => Shell.Current.GoToAsync("//auth_login");

        [RelayCommand]
        void SignUpPage() => Shell.Current.GoToAsync("//auth_signup");

        public bool isLoggedIn()
        {
            return _authenticationService.TryAutoLogin();
        }
    }
}
