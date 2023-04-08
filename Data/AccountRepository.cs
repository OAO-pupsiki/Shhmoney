using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class AccountRepository
    {
        private readonly DbContext _dbContext;

        public AccountRepository()
        {
            _dbContext = DbContext.GetDbContext();
        }

        public void AddAccount(Account account)
        {
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
        }

        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.SingleOrDefault(a => a.Id == id);
        }

        public List<Account> GetAllAccounts()
        {
            return _dbContext.Accounts.ToList();
        }
    }
}
