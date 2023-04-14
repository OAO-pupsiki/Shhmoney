using Shhmoney.Models;
using Shhmoney.Data;

namespace Shhmoney.Services
{
    public class CategoryService
    {
        private readonly IncomeCategoryRepository _incomeCategoryRepository;
        private readonly ExpenseCategoryRepository _expenseCategoryRepository;

        public CategoryService()
        {
            _incomeCategoryRepository = new IncomeCategoryRepository();
            _expenseCategoryRepository = new ExpenseCategoryRepository();
        }

        public void AddExpenseCategory(ExpenseCategory category)
        {
            _expenseCategoryRepository.AddExpenseCategory(category);
        }

        public void AddIncomeCategory(IncomeCategory category)
        {
            _incomeCategoryRepository.AddIncomeCategory(category);
        }
    }
}
