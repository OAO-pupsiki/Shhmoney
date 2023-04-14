using Shhmoney.Models;
using Shhmoney.Data;
using Shhmoney.Utils;

namespace Shhmoney.Services
{
    public class TransactionService
    {
        private readonly IncomeCategoryRepository _incomeCategoryRepository;
        private readonly ExpenseCategoryRepository _expenseCategoryRepository;

        public TransactionService()
        {
            _incomeCategoryRepository = new IncomeCategoryRepository();
            _expenseCategoryRepository = new ExpenseCategoryRepository();
        }

        public List<IncomeCategory> GetIncomeCategoriesByUser(User user)
        {
            return _incomeCategoryRepository.GetIncomeCategoriesByUserId(user.Id);
        }

        public void AddIncomeCategory(string name, string description, User user)
        {
            if (user == null)
                throw new Exception("User is null");

            var incomeCategory = new IncomeCategory
            {
                Name = name,
                Description = description,
                User = user
            };

            _incomeCategoryRepository.AddIncomeCategory(incomeCategory);
        }

        public void ChangeIncomeCategory(int incomeCategoryId, string name, string description)
        {
            var incomeCategory = _incomeCategoryRepository.GetIncomeCategory(incomeCategoryId);

            if (incomeCategory == null)
                throw new Exception("Unable to load income category");

            incomeCategory.Name = name;
            incomeCategory.Description = description;

            _incomeCategoryRepository.UpdateIncomeCategory(incomeCategoryId);
        }

        public void DeleteIncomeCategory(int incomeCategoryId, User user)
        {
            var incomeCategory = _incomeCategoryRepository.GetIncomeCategory(incomeCategoryId);

            if (incomeCategory == null)
                throw new Exception("Unable to load income category");

            if (incomeCategory.User != user)
                throw new Exception("No rights to delete income category");

            _incomeCategoryRepository.DeleteIncomeCategory(incomeCategoryId);
        }

        public List<ExpenseCategory> GetExpenseCategoriesByUser(User user)
        {
            return _expenseCategoryRepository.GetExpenseCategoriesByUserId(user.Id);
        }

        public void AddExpenseCategory(string name, string description, User user)
        {
            if (user == null)
                throw new Exception("User is null");

            var expenseCategory = new ExpenseCategory
            {
                Name = name,
                Description = description,
                User = user
            };

            _expenseCategoryRepository.AddExpenseCategory(expenseCategory);
        }

        public void ChangeExpenseCategory(int expenseCategoryId, string name, string description)
        {
            var expenseCategory = _expenseCategoryRepository.GetExpenseCategory(expenseCategoryId);

            if (expenseCategory == null)
                throw new Exception("Unable to load income category");

            expenseCategory.Name = name;
            expenseCategory.Description = description;

            _expenseCategoryRepository.UpdateExpenseCategory(expenseCategoryId);
        }

        public void DeleteExpenseCategory(int expenseCategoryId, User user)
        {
            var expenseCategory = _expenseCategoryRepository.GetExpenseCategory(expenseCategoryId);

            if (expenseCategory == null)
                throw new Exception("Unable to load income category");

            if (expenseCategory.User != user)
                throw new Exception("No rights to delete income category");

            _expenseCategoryRepository.DeleteExpenseCategory(expenseCategoryId);
        }
    }
}
