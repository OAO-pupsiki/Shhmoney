using System;
using System.Collections.Generic;
using Shhmoney.Data;

namespace Shhmoney
{
    internal class Information_of_payment
    {
        private UserRepository _userRepository { get; set; }

        public Information_of_payment(UserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public void Card(decimal value)
        {
            int id = 1; //Здесь должен браться id текущего пользователя (метод егора)
            var user = _userRepository.GetUserById(id);
        }

        public void Cash(decimal value) {
            int id = 1; //Здесь должен браться id текущего пользователя (метод егора)
            var user = _userRepository.GetUserById(id);
        }
    }
}
