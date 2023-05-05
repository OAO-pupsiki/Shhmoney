using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shhmoney.Data;
using Shhmoney.Models;

namespace Shhmoney.Services
{
    class AccountService
    {
        private readonly AccountRepository _accountRepository;

        public AccountService()
        {
            _accountRepository = new AccountRepository();
        }
        public Account AddAccount(Account account)
        {
            var validationContext = new ValidationContext(account);
            var validationResults = new List<ValidationResult>();

           /* if (!Validator.TryValidateObject(account, validationContext, validationResults, true))
            {
                throw new ArgumentException($"Invalid account object. Validation failed. Errors: {string.Join(", ", validationResults)}");
            }*/
            return _accountRepository.AddAccount(account);
        }

        public Account GetAccountById(int id)
        {
            return _accountRepository.GetAccountById(id);
        }

        public List<Account> GetAllAccounts()
        {
            return _accountRepository.GetAllAccounts();
        }
        /*
        public void UpdateAccount(Account account)
        {
            // TODO: validate account object before updating it in the database
            _accountRepository.UpdateAccount(account);
        }

        public void DeleteAccount(Account account)
        {
            _accountRepository.DeleteAccount(account);
        }*/
    }
}
    