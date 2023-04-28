using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shhmoney.Data;

namespace Shhmoney
{
    internal class ChangeUser
    {
        public static void ChangeUserById(string Username, string Email, string Password)
        {
            int id = 1; //Здесь должен браться id текущего пользователя (метод егора)
            UserRepository UserRep = new UserRepository();
            var user = UserRep.GetUserById(id);
            user.Username = Username;
            user.Email = Email; 
            user.Password = Password;
            UserRep.SaveChanges();
        }
    }
    
    public enum UserStatus
    {
        Active,
        Inactive
    }
    internal class ChangeAdmin
    {
        public static void ChangeAdminById(string Email, DateTime Expiration, UserStatus Status)
        {
            int id = 1; //Здесь должен браться id admin
            UserRepository UserRep = new UserRepository();
            var User = UserRep.GetUserByEmail(Email);
            var UserSessionRep = new UserSessionRepository();
            var UserSession = UserSessionRep.GetSessionByUserId(User.Id);
            UserSession.Expiration = Expiration;
            // как мы храним статус?
            // изменить статус
            UserRep.SaveChanges();
        }
    }
}
