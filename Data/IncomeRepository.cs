using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class IncomeRepository
    {
        private readonly DbContext _dbContext;

        public IncomeRepository()
        {
            _dbContext = DbContext.GetDbContext();
        }

        public void AddIncome(Income income)
        {
            _dbContext.Incomes.Add(income);
            _dbContext.SaveChanges();
        }

        public Income GetIncomeById(int id)
        {
            return _dbContext.Incomes.SingleOrDefault(i => i.Id == id);
        }

        public List<Income> GetIncomesByAccount(Account account)
        {
            return _dbContext.Incomes.Where(i =>  i.Account == account).ToList();
        }

        public List<Income> GetIncomesByCategory(Category category)
        {
            return _dbContext.Incomes.Where(i => i.Category == category).ToList();
        }
    }
}
