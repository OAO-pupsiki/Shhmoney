using Shhmoney.Data;
using Shhmoney.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shhmoney.Services
{
    internal class ExpenseService
    {
        private readonly ExpenseRepository _expenseRepository;

        public ExpenseService()
        {
            _expenseRepository = new ExpenseRepository();
        }

        public void AddExpense(Expense expense)
        {
            _expenseRepository.AddExpense(expense);
        }

        public void DelExpenseByCategory(Category category)
        {
            int userId = 1; // здесь должен браться id текущего пользователя
            var lstExp = _expenseRepository.GetExpensesByCategory(category);
            foreach (var exp in lstExp.Where(exp => exp.Account.UserId == userId))
            {
                _expenseRepository.DeleteExpense(exp.Account.UserId);
            }
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
