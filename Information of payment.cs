using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shhmoney.Data;

namespace Shhmoney
{
    internal class Information_of_payment
    {
        public static void Card(decimal value)
        {
            int id = 1; //Здесь должен браться id текущего пользователя (метод егора)
            UserRepository UserRep = new UserRepository();
            var user = UserRep.GetUserById(id);
             



        }



        public static void Cash(decimal value) {
            int id = 1; //Здесь должен браться id текущего пользователя (метод егора)
            UserRepository UserRep = new UserRepository();
            var user = UserRep.GetUserById(id);





        }
    }
}
