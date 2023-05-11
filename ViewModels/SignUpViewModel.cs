using Shhmoney.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Shhmoney.ViewModels
{
    public partial class SignUpViewModel : ObservableObject
    {
        private readonly AuthenticationService _authenticationService;

        public SignUpViewModel(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string confirmedPassword;

        [ObservableProperty]
        string email;

        [RelayCommand]
        void SignUp()
        {
            if (_authenticationService.SignUp(Username, Password, Email))
            {
                Shell.Current.GoToAsync("//auth/login");
            }
        }

        [RelayCommand]
        void LoginPage()
        {
            Shell.Current.GoToAsync("//auth/login");
        }
    }
}
