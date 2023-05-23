using System;
using System.Collections.Generic;
using Shhmoney.Models;
using Shhmoney.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Shhmoney.Data;
using Shhmoney.Utils;

namespace Shhmoney.ViewModels
{
    public class UserViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly UserService _userService;
        private readonly UserRepository _userRepository;
        private string _newName;
        private string _confirmedPassword;
        public UserViewModel(UserService userService, UserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
            User = new User
            {
                Id = Utils.AppContext.CurrentUser.Id,
                Username = Utils.AppContext.CurrentUser.Username,
                Password = Utils.AppContext.CurrentUser.Password,
                Email = Utils.AppContext.CurrentUser.Email,
                RoleId = Utils.AppContext.CurrentUser.RoleId,
                Role = Utils.AppContext.CurrentUser.Role,
                Accounts = Utils.AppContext.CurrentUser.Accounts
            };
            SaveChangesCommand = new Command(SaveChanges);
        }

        public User User { get; set; }
        public ICommand SaveChangesCommand { get; set; }

        private async void SaveChanges()
        {
            bool result = await Shell.Current.DisplayAlert("Подтвердить действие", "Вы хотите изменить пароль?", "Да", "Нет");

            if (result)
            {
                var existingUser = _userRepository.GetUserById(Utils.AppContext.CurrentUser.Id);
                var hashedNewPassword = PasswordHasher.HashPassword(NewPassword);

                if (existingUser.Password == hashedNewPassword)
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Новый пароль не может совпадать с текущим паролем.", "ОК");
                }
                else if (NewPassword != ConfirmedPassword)
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Подтверждение пароля не совпадает.", "ОК");
                }
                else
                {
                    existingUser.Password = hashedNewPassword;
                    _userRepository.UpdateUser(existingUser);

                    await Shell.Current.DisplayAlert("Уведомление", "Пароль успешно сохранен. Используйте его при следующей авторизации.", "ОК");
                    NewPassword = string.Empty;
                    ConfirmedPassword = string.Empty;
                }

            }
        }


        public string NewPassword
        {
            get => _newName;
            set
            {
                if (_newName == value)
                    return;
                _newName = value;
                OnProperyChanged();
            }
        }
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
        public void OnProperyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
