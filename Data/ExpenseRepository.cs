using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class ExpenseRepository
    {
        private readonly DbContext _dbContext;

        public ExpenseRepository()
        {
            _dbContext = DbContext.GetDbContext();
        }

        public void AddExpense(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            _dbContext.SaveChanges();
        }

        public Expense GetExpensesById(int id)
        {
            return _dbContext.Expenses.SingleOrDefault(e => e.Id == id);
        }

        public List<Expense> GetExpensesByAccount(Account account)
        {
            return _dbContext.Expenses.Where(e => e.Account == account).ToList();
        }

        public List<Expense> GetExpensesByCategory(Category category)
        {
            return _dbContext.Expenses.Where(e => e.ExpenseCategory == category).ToList();
        }


        public void DeleteExpense(int id)
        {
            var expense = GetExpensesById(id);
            _dbContext.Expenses.Remove(expense);
            _dbContext.SaveChanges();
        }

        public void UpdateExpense(int id)
        {
            var expense = GetExpensesById(id);
            _dbContext.Expenses.Update(expense);
            _dbContext.SaveChanges();
        }
    }
}
