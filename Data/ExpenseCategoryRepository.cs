using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class ExpenseCategoryRepository
    {
        private readonly DbContext _dbContext;

        public ExpenseCategoryRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ExpenseCategory AddExpenseCategory(ExpenseCategory expenseCategory)
        {
            var dbItem = _dbContext.ExpenseCategories.Add(expenseCategory);
            _dbContext.SaveChanges();
            return dbItem.Entity;
        }

        public void DeleteExpenseCategory(ExpenseCategory expenseCategory)
        {
            _dbContext.ExpenseCategories.Remove(expenseCategory);
            _dbContext.SaveChanges();
        }

        public void UpdateExpenseCategory(ExpenseCategory expenseCategory)
        {
            _dbContext.ExpenseCategories.Update(expenseCategory);
            _dbContext.SaveChanges();
        }

        public ExpenseCategory GetExpenseCategory(int id)
        {
            return _dbContext.ExpenseCategories.SingleOrDefault(c => c.Id == id);
        }

        public List<ExpenseCategory> GetExpenseCategoriesByUserId(int id)
        {
            return _dbContext.ExpenseCategories.Where(c => c.UserId == id).ToList();
        }
    }
}
