
using AndroidX.ConstraintLayout.Core.Motion.Utils;
using Moq;
using NUnit.Framework;
using Shhmoney.Data;
using Shhmoney.Models;
using Shhmoney.Services;


namespace Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<UserRepository> _userRepositoryMock;
        private Mock<AccountRepository> _accountRepositoryMock;
        private Mock<IncomeRepository> _incomeRepositoryMock;
        private Mock<ExpenseRepository> _expenseRepositoryMock;
        private Mock<IncomeCategoryRepository> _incomeCategoryRepositoryMock;
        private Mock<ExpenseCategoryRepository> _expenseCategoryRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<UserRepository>();
            _accountRepositoryMock = new Mock<AccountRepository>();
            _incomeRepositoryMock = new Mock<IncomeRepository>();
            _expenseRepositoryMock = new Mock<ExpenseRepository>();
            _incomeCategoryRepositoryMock = new Mock<IncomeCategoryRepository>();
            _expenseCategoryRepositoryMock = new Mock<ExpenseCategoryRepository>();

            _userService = new UserService(
                _userRepositoryMock.Object,
                _accountRepositoryMock.Object,
                _incomeRepositoryMock.Object,
                _expenseRepositoryMock.Object,
                _incomeCategoryRepositoryMock.Object,
                _expenseCategoryRepositoryMock.Object
            );
        }

        [Test]
        public void AddAccount_CallsAddAccountOnAccountRepository()
        {
            // Arrange
            var account = new Account();

            // Act
            _userService.AddAccount(account);

            // Assert
            _accountRepositoryMock.Verify(r => r.AddAccount(account), Times.Once);
        }

        [Test]
        public void RemoveAccount_CallsRemoveAccountOnAccountRepository()
        {
            // Arrange
            var account = new Account();

            // Act
            _userService.RemoveAccount(account);

            // Assert
            _accountRepositoryMock.Verify(r => r.RemoveAccount(account), Times.Once);
        }

        [Test]
        public void GetIncomes_ReturnsIncomesForAccount()
        {
            // Arrange
            var account = new Account();
            var expectedIncomes = new List<Income> { new Income(), new Income() };
            _incomeRepositoryMock.Setup(r => r.GetIncomesByAccount(account)).Returns(expectedIncomes);

            // Act
            var actualIncomes = _userService.GetIncomes(account);

            // Assert
            Assert.AreEqual(expectedIncomes, actualIncomes);
        }
      
        [Test]
        public void AddIncome_CallsAddIncomeOnIncomeRepository()
        {
            // Arrange
            var income = new Income();

            // Act
            _userService.AddIncome(income);

            // Assert
            _incomeRepositoryMock.Verify(r => r.AddIncome(income), Times.Once);
        }

        [Test]
        public void RemoveIncome_CallsRemoveIncomeOnIncomeRepository()
        {
            // Arrange
            var income = new Income();

            // Act
            _userService.RemoveIncome(income);

            // Assert
            _incomeRepositoryMock.Verify(r => r.RemoveIncome(income), Times.Once);
        }

        [Test]
        public void GetExpenses_ReturnsExpensesForAccount()
        {
            // Arrange
            var account = new Account();
            var expectedExpenses = new List<Expense> { new Expense(), new Expense() };
            _expenseRepositoryMock.Setup(r => r.GetExpensesByAccount(account)).Returns(expectedExpenses);

            // Act
            var actualExpenses = _userService.GetExpenses(account);

            // Assert
            CollectionAssert.AreEqual(expectedExpenses, actualExpenses);
        }

        [Test]
        public void AddExpense_CallsAddExpenseOnExpenseRepository()
        {
            // Arrange
            var expense = new Expense();

            // Act
            _userService.AddExpense(expense);

            // Assert
            _expenseRepositoryMock.Verify(r => r.AddExpense(expense), Times.Once);
        }

    }

}