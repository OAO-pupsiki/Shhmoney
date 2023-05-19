using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shhmoney.Data;

namespace Shhmoney
{
    public class ChangeUser
    {
        private UserRepository _userRepository { get; set; }
        public ChangeUser(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void ChangeUserById(string Username, string Email, string Password)
        {
            int id = 1; //Здесь должен браться id текущего пользователя (метод егора)
            var user = _userRepository.GetUserById(id);
            user.Username = Username;
            user.Email = Email; 
            user.Password = Password;
            _userRepository.SaveChanges();
        }
    }
    
    public enum UserStatus
    {
        Active,
        Inactive
    }
    internal class ChangeAdmin
    {
        private UserRepository _userRepository { get; set; }
        private UserSessionRepository _userSessionRepository { get; set; }
        public ChangeAdmin (UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void ChangeAdminById(string Email, DateTime Expiration, UserStatus Status)
        {
            int id = 1; //Здесь должен браться id admin
            var User = _userRepository.GetUserByEmail(Email);
            var UserSession = _userSessionRepository.GetSessionByUserId(User.Id);
            UserSession.Expiration = Expiration;
            // как мы храним статус?
            // изменить статус
            _userRepository.SaveChanges();
        }
    }
}
