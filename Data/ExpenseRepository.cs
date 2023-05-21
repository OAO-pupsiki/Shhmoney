using Microsoft.EntityFrameworkCore;
using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class ExpenseRepository
    {
        private readonly DbContext _dbContext;

        public ExpenseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddExpense(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            _dbContext.SaveChanges();
        }

        public void RemoveExpense(Expense expense)
        {
            _dbContext.Expenses.Remove(expense);
            _dbContext.SaveChanges();
        }

        public void UpdateExpense(Expense expense)
        {
            //_dbContext.Expenses.Update(expense);
            _dbContext.SaveChanges();
        }

        public void DeleteExpenseByUserId(int userId)
        {
            var list = _dbContext.Expenses.Where(e => e.UserId == userId).ToList();

            _dbContext.Expenses.RemoveRange(list);
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

        public List<Expense> GetExpensesByUser(User user)
        {
            return _dbContext.Expenses
                .Include(e => e.Account)
                .Include(e => e.ExpenseCategory)
                .Where(e => e.UserId == user.Id)
                .ToList();
        }

        public List<Expense> GetExpensesByCategory(Category category)
        {
            return _dbContext.Expenses.Where(e => e.ExpenseCategory == category).ToList();
        }
    }
}
