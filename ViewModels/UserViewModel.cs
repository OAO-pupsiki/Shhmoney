using System;
using System.Collections.Generic;
using Shhmoney.Models;
using Shhmoney.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Shhmoney.ViewModels
{
    public class UserViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly UserService _userService;
        public UserViewModel(UserService userService)
        {
            _userService = userService;
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
            ChangeUserCommand = new Command(ChangeUser);
            SaveChangesCommand = new Command(SaveChanges);

        }

        public User User { get; set; }

        public string NewPassword { get; set; }
        public string NewEmail { get; set; }

        public ICommand ChangeUserCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }


        private void ChangeUser()
        {
            Utils.AppContext.CurrentUser.Password = NewPassword;
            Utils.AppContext.CurrentUser.Email = NewEmail;
            _userService.ChangeUser(Utils.AppContext.CurrentUser);
        }
        private void SaveChanges()
        {
            _userService.ChangeUser(Utils.AppContext.CurrentUser);
        }
    }
}