using Shhmoney.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Shhmoney.Data
{
    public class AccountRepository
    {
        private readonly DbContext _dbContext;

        public AccountRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Account AddAccount(Account account)
        {
            var dbItem = _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            return dbItem.Entity;
        }

        public void RemoveAccount(Account account)
        {
            _dbContext.Accounts.Remove(account);
            _dbContext.SaveChanges();
        }

        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.SingleOrDefault(a => a.Id == id);
        }

        public List<Account> GetAccountsByUser(int userId)
        {
            return _dbContext.Accounts.Where(a => a.UserId == userId).ToList();
        }

        public List<Account> GetAllAccounts()
        {
            return _dbContext.Accounts.ToList();
        }
    }
}
