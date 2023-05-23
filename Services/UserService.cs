using Shhmoney.Models;
using Shhmoney.Data;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Linq;
using Shhmoney.Utils;

namespace Shhmoney.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly AccountRepository _accountRepository;
        private readonly IncomeRepository _incomeRepository;
        private readonly ExpenseRepository _expenseRepository;
        private readonly IncomeCategoryRepository _incomeCategoryRepository;
        private readonly ExpenseCategoryRepository _expenseCategoryRepository;

        public UserService(UserRepository userRepository, AccountRepository accountRepository, IncomeRepository incomeRepository, ExpenseRepository expenseRepository, IncomeCategoryRepository incomeCategoryRepository, ExpenseCategoryRepository expenseCategoryRepository)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _incomeCategoryRepository = incomeCategoryRepository;
            _expenseCategoryRepository = expenseCategoryRepository;
            _incomeCategoryRepository = incomeCategoryRepository;
            _expenseCategoryRepository = expenseCategoryRepository;
        }

        public List<Account> GetAccounts()
        {
            return new List<Account>(_accountRepository.GetAccountsByUser(Utils.AppContext.CurrentUser.Id));
        }

        public void AddAccount(Account account)
        {
            _accountRepository.AddAccount(account);
        }

        public void RemoveAccount(Account account)
        {
            _accountRepository.RemoveAccount(account);
        }

        public List<Income> GetIncomes(Account account)
        {
            return new List<Income>(_incomeRepository.GetIncomesByAccount(account));
        }

        public List<Transaction> GetUserTransactions(User user) => new List<Transaction>
                (
                    _incomeRepository.GetIncomesByUser(user)
                    .Concat<Transaction>(_expenseRepository.GetExpensesByUser(user))
                    .OrderBy(t => t.DateTime)
                    .ToList()
                );

        public void AddIncome(Income income)
        {
            _incomeRepository.AddIncome(income);
        }

        public void RemoveIncome(Income income)
        {
            _incomeRepository.RemoveIncome(income);
        }

        public List<Expense> GetExpenses(Account account)
        {
            return new List<Expense>(_expenseRepository.GetExpensesByAccount(account));
        }

        public void AddExpense(Expense expense)
        {
            _expenseRepository.AddExpense(expense);
        }

        public void RemoveExpense(Expense expense)
        {
            _expenseRepository.RemoveExpense(expense);
        }

        public List<IncomeCategory> GetInocmeCategories()
        {
            return _incomeCategoryRepository.GetIncomeCategoriesByUserId(Utils.AppContext.CurrentUser.Id);
        }

        public List<ExpenseCategory> GetExpenseCategories()
        {
            return _expenseCategoryRepository.GetExpenseCategoriesByUserId(Utils.AppContext.CurrentUser.Id);
        }
        public void ChangeUser(string password)
        {
            var existingUser = _userRepository.GetUserById(Utils.AppContext.CurrentUser.Id);
            var hashedPassword = PasswordHasher.HashPassword(password);
            existingUser.Password = hashedPassword;
            _userRepository.UpdateUser(existingUser);
        }
    }
}

