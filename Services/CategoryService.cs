using Shhmoney.Models;
using Shhmoney.Data;

namespace Shhmoney.Services
{
    public class CategoryService
    {
        private readonly IncomeCategoryRepository _incomeCategoryRepository;
        private readonly ExpenseCategoryRepository _expenseCategoryRepository;

        public CategoryService(IncomeCategoryRepository incomeCategoryRepository, ExpenseCategoryRepository expenseCategoryRepository)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
            _expenseCategoryRepository = expenseCategoryRepository;
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
