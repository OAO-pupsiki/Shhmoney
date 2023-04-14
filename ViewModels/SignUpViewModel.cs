using Shhmoney.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Shhmoney.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SignUpCommand { get; set; }
        public ICommand LoginPageCommand { get; set; }

        private readonly AuthenticationService _authenticationService;
        private string _username;
        private string _email;
        private string _password;
        private string _confirmedPassword;

        public SignUpViewModel()
        {
            _authenticationService = new AuthenticationService();
            SignUpCommand = new Command(() =>
            {
                if (_authenticationService.SignUp(Username, Password, Email))
                {
                    //Redirect to the login page
                    Shell.Current.GoToAsync("//auth/login");
                }
                else
                {

                }
            });
            LoginPageCommand = new Command(() =>
            {
                Shell.Current.GoToAsync("//auth/login");
            });
        }

        [Required(ErrorMessage = "Please enter a username.")]
        public string Username
        {
            get => _username;
            set
            {
                if (_username == value)
                    return;
                _username = value;
                OnProperyChanged();
            }
        }

        [Required(ErrorMessage = "Please enter a password.")]
        public string Password
        {
            get => _password;
            set
            {
                if (_password == value)
                    return;
                _password = value;
                OnProperyChanged();
            }
        }

        [Required(ErrorMessage = "Please confirm your password.")]
        public string ConfirmedPassword
        {
            get => _confirmedPassword;
            set
            {
                if (_confirmedPassword == value)
                    return;
                _confirmedPassword = value;
                OnProperyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email == value)
                    return;
                _email = value;
                OnProperyChanged();
            }
        }

        public void OnProperyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
