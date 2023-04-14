using Shhmoney.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Shhmoney.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoginCommand { get; set; }
        public ICommand SignUpPageCommand { get; set; }

        private readonly AuthenticationService _authenticationService;
        private string _username;
        private string _password;
        private bool _rememberMe;

        public LoginViewModel()
        {
            _authenticationService = new AuthenticationService();
            LoginCommand = new Command(() =>
            {

                try
                {
                    _authenticationService.Login(Username, Password, RememberMe);
                    Shell.Current.GoToAsync("//home/categories");
                }
                catch(Exception e)
                {
                    
                }
            });
            SignUpPageCommand = new Command(() =>
            {
                Shell.Current.GoToAsync("//auth/signup");
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

        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                if (_rememberMe == value)
                    return;
                _rememberMe = value;
                OnProperyChanged();
            }
        }

        public void OnProperyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
