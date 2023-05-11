using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class IncomeCategoryRepository
    {
        private readonly DbContext _dbContext;

        public IncomeCategoryRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddIncomeCategory(IncomeCategory incomeCategory)
        {
            _dbContext.IncomeCategories.Add(incomeCategory);
            _dbContext.SaveChanges();
        }

        public void DeleteIncomeCategory(int id)
        {
            var incomeCategory = GetIncomeCategory(id);
            _dbContext.IncomeCategories.Remove(incomeCategory);
            _dbContext.SaveChanges();
        }

        public void UpdateIncomeCategory(int id)
        {
            var incomeCategory = GetIncomeCategory(id);
            _dbContext.IncomeCategories.Update(incomeCategory);
            _dbContext.SaveChanges();
        }

        public IncomeCategory GetIncomeCategory(int id)
        {
            return _dbContext.IncomeCategories.SingleOrDefault(c => c.Id == id);
        }

        public List<IncomeCategory> GetIncomeCategoriesByUserId(int id)
        {
            return _dbContext.IncomeCategories.Where(c => c.Id == id).ToList();
        }
    }
}
