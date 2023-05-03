using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class IncomeCategoryRepository
    {
        private readonly DbContext _dbContext;

        public IncomeCategoryRepository()
        {
            _dbContext = DbContext.GetDbContext();
        }

        public IncomeCategory AddIncomeCategory(IncomeCategory incomeCategory)
        {
            var dbItem = _dbContext.IncomeCategories.Add(incomeCategory);
            _dbContext.SaveChanges();
            return dbItem.Entity;
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
            return _dbContext.IncomeCategories.Where(c => c.UserId == id).ToList();
        }
    }
}
