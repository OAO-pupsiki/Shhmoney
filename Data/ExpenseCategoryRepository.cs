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

        public void AddExpenseCategory(ExpenseCategory expenseCategory)
        {
            _dbContext.ExpenseCategories.Add(expenseCategory);
            _dbContext.SaveChanges();
        }

        public void DeleteExpenseCategory(int id)
        {
            var expenseCategory = GetExpenseCategory(id);
            _dbContext.ExpenseCategories.Remove(expenseCategory);
            _dbContext.SaveChanges();
        }

        public void UpdateExpenseCategory(int id)
        {
            var expenseCategory = GetExpenseCategory(id);
            _dbContext.ExpenseCategories.Update(expenseCategory);
            _dbContext.SaveChanges();
        }

        public ExpenseCategory GetExpenseCategory(int id)
        {
            return _dbContext.ExpenseCategories.SingleOrDefault(c => c.Id == id);
        }

        public List<ExpenseCategory> GetExpenseCategoriesByUserId(int id)
        {
            return _dbContext.ExpenseCategories.Where(c => c.Id == id).ToList();
        }
    }
}
