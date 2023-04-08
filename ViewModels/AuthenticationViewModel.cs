using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace Shhmoney.ViewModels
{
    public class AuthenticationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoginPageCommand { get; set; }
        public ICommand SignUpPageCommand { get; set; }

        public AuthenticationViewModel() 
        {
            LoginPageCommand = new Command(() =>
            {
                Shell.Current.GoToAsync("//auth/login");
            });
            SignUpPageCommand = new Command(() =>
            {
                Shell.Current.GoToAsync("//auth/signup");
            });
        }
    }
}
