using Shhmoney.Services;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace Shhmoney.ViewModels
{
    public class AuthenticationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SubmitCommand { get; set; }

        private readonly AuthenticationService _authenticationService;
        private string _username;
        private string _password;

        public AuthenticationViewModel()
        {
            _authenticationService = new AuthenticationService();
            SubmitCommand = new Command(() =>
            {
                if (_authenticationService.Login(Username, Password, true))
                {
                    //Redirect to the main page
                }
                else
                {
                    
                }
            });
        }

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

        public void OnProperyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
