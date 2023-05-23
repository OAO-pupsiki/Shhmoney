using Shhmoney.Data;
using Shhmoney.Models;

namespace Shhmoney.Services
{
    internal class ExpenseService
    {
        private readonly ExpenseRepository _expenseRepository;

        public ExpenseService(ExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public void AddExpense(Expense expense)
        {
            _expenseRepository.AddExpense(expense);
        }

        public void DelExpenseByCategory(Category category)
        {
             _expenseRepository.DeleteExpenseByUserId(category.UserId);
        }

        public List<Expense> ViewExpenseFromCategory(Category category)
        {
            return _expenseRepository.GetExpensesByCategory(category).ToList();
        }

        public void ChangeExpense(Category category, Expense expense)
        {

        }
    }
}
