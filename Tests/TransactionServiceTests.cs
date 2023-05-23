using Android.Icu.Text;
using Android.Media;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Shhmoney.Data;
using Shhmoney.Models;


namespace Shhmoney.Services.Tests
{
    [TestFixture]
    public class TransactionServiceTests
    {
        private TransactionService _transactionService;
        private Mock<IncomeCategoryRepository> _incomeCategoryRepositoryMock;
        private Mock<ExpenseCategoryRepository> _expenseCategoryRepositoryMock;
        private User _user;
        private object _expenseCategoryRepository;

        [SetUp]
        public void SetUp()
        {
            _incomeCategoryRepositoryMock = new Mock<IncomeCategoryRepository>();
            _expenseCategoryRepositoryMock = new Mock<ExpenseCategoryRepository>();
            _transactionService = new TransactionService(_incomeCategoryRepositoryMock.Object, _expenseCategoryRepositoryMock.Object);
        }

        [Test]
        public void AddIncomeCategory_ValidData_ReturnsIncomeCategory()
        {
            // Arrange
            var name = "Salary";
            var description = "Monthly salary";
            var user = new User { Id = 1 };
            var incomeCategory = new IncomeCategory { Name = name, Description = description, User = user };
            _incomeCategoryRepositoryMock.Setup(x => x.AddIncomeCategory(It.IsAny<IncomeCategory>())).Returns(incomeCategory);

            // Act
            var result = _transactionService.AddIncomeCategory(name, description, user);

            // Assert
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(description, result.Description);
            Assert.AreEqual(user, result.User);
            _incomeCategoryRepositoryMock.Verify(x => x.AddIncomeCategory(It.IsAny<IncomeCategory>()), Times.Once);
        }

        [Test]
        public void ChangeIncomeCategory_ValidData_ChangesIncomeCategory()
        {
            // Arrange
            var name = "New Name";
            var description = "New Description";
            var incomeCategoryId = 1;
            var incomeCategory = new IncomeCategory { Id = incomeCategoryId, Name = "Old Name", Description = "Old Description", User = new User { Id = 1 } };
            _incomeCategoryRepositoryMock.Setup(x => x.GetIncomeCategory(incomeCategoryId)).Returns(incomeCategory);

            // Act
            _transactionService.ChangeIncomeCategory(incomeCategoryId, name, description);

            // Assert
            Assert.AreEqual(name, incomeCategory.Name);
            Assert.AreEqual(description, incomeCategory.Description);
            _incomeCategoryRepositoryMock.Verify(x => x.UpdateIncomeCategory(incomeCategoryId), Times.Once);
        }

        [Test]
        public void DeleteIncomeCategory_ValidData_DeletesIncomeCategory()
        {
            // Arrange
            var incomeCategoryId = 1;
            var user = new User { Id = 1 };
            var incomeCategory = new IncomeCategory { Id = incomeCategoryId, Name = "Salary", Description = "Monthly salary", User = user };
            _incomeCategoryRepositoryMock.Setup(x => x.GetIncomeCategory(incomeCategoryId)).Returns(incomeCategory);

            // Act
            _transactionService.DeleteIncomeCategory(incomeCategoryId, user);

            // Assert
            _incomeCategoryRepositoryMock.Verify(x => x.DeleteIncomeCategory(incomeCategory), Times.Once);
        }

        [Test]
        public void DeleteIncomeCategory_IncomeCategoryNotFound_ThrowsException()
        {
            // Arrange
            var incomeCategoryId = 1;
            var user = new User { Id = 1 };
            IncomeCategory incomeCategory = null;
            _incomeCategoryRepositoryMock.Setup(x => x.GetIncomeCategory(incomeCategoryId)).Returns(incomeCategory);

            // Act & Assert
            Assert.Throws<Exception>(() => _transactionService.DeleteIncomeCategory(incomeCategoryId, user));
            _incomeCategoryRepositoryMock.Verify(x => x.DeleteIncomeCategory(It.IsAny<IncomeCategory>()), Times.Never);
        }


        [Test]
        public void GetExpenseCategoriesByUser_WithValidUser_ReturnsExpenseCategories()
        {
            // Arrange
            var user = new User { Id = 1 };
            var expenseCategories = new List<ExpenseCategory> { new ExpenseCategory { Id = 1, Name = "Expense 1", User = user } };
            _expenseCategoryRepositoryMock.Setup(m => m.GetExpenseCategoriesByUserId(user.Id)).Returns(expenseCategories);

            // Act
            var result = _transactionService.GetExpenseCategoriesByUser(user);

            // Assert
            Assert.AreEqual(expenseCategories, result);
        }

        [Test]
        public void AddExpenseCategory_WithValidData_ReturnsExpenseCategory()
        {
            // Arrange
            var user = new User { Id = 1 };
            var expenseCategory = new ExpenseCategory { Name = "Expense 1", Description = "Expense description", User = user };
            _expenseCategoryRepositoryMock.Setup(m => m.AddExpenseCategory(expenseCategory)).Returns(expenseCategory);

            // Act
            var result = _transactionService.AddExpenseCategory(expenseCategory.Name, expenseCategory.Description, user);

            // Assert
            Assert.AreEqual(expenseCategory, result);
        }

        [Test]
        public void ChangeExpenseCategory_WithValidData_UpdatesExpenseCategory()
        {
            // Arrange
            var expenseCategoryId = 1;
            var name = "New Expense Name";
            var description = "New Expense Description";
            var expenseCategory = new ExpenseCategory { Id = expenseCategoryId, Name = "Expense 1", Description = "Expense description", User = new User() };
            _expenseCategoryRepositoryMock.Setup(m => m.GetExpenseCategory(expenseCategoryId)).Returns(expenseCategory);

            // Act
            _transactionService.ChangeExpenseCategory(expenseCategoryId, name, description);

            // Assert
            Assert.AreEqual(name, expenseCategory.Name);
            Assert.AreEqual(description, expenseCategory.Description);
            _expenseCategoryRepositoryMock.Verify(m => m.UpdateExpenseCategory(expenseCategory), Times.Once);
        }

        [Test]
        public void ChangeExpenseCategory_WithInvalidExpenseCategoryId_ThrowsException()
        {
            // Arrange
            var expenseCategoryId = 1;
            var name = "New Expense Name";
            var description = "New Expense Description";
            _expenseCategoryRepositoryMock.Setup(m => m.GetExpenseCategory(expenseCategoryId)).Returns((ExpenseCategory)null);

            // Act and Assert
            Assert.Throws<Exception>(() => _transactionService.ChangeExpenseCategory(expenseCategoryId, name, description));
        }

        [Test]
        public void DeleteExpenseCategory_WithValidInput_ShouldCallRepository()
        {
            // Arrange
            int expenseCategoryId = 1;
            User user = new User { Id = 1 };
            ExpenseCategory expenseCategory = new ExpenseCategory { Id = expenseCategoryId, User = user };
            Mock<ExpenseCategoryRepository> expenseCategoryRepositoryMock = new Mock<ExpenseCategoryRepository>();
            expenseCategoryRepositoryMock.Setup(x => x.GetExpenseCategory(expenseCategoryId)).Returns(expenseCategory);
            TransactionService transactionService = new TransactionService(null, expenseCategoryRepositoryMock.Object);

            // Act
            transactionService.DeleteExpenseCategory(expenseCategoryId, user);

            // Assert
            expenseCategoryRepositoryMock.Verify(x => x.DeleteExpenseCategory(expenseCategory), Times.Once);
        }

        [Test]
        public void DeleteExpenseCategory_WithInvalidCategoryId_ShouldThrowException()
        {
            // Arrange
            int expenseCategoryId = 1;
            User user = new User { Id = 1 };
            Mock<ExpenseCategoryRepository> expenseCategoryRepositoryMock = new Mock<ExpenseCategoryRepository>();
            expenseCategoryRepositoryMock.Setup(x => x.GetExpenseCategory(expenseCategoryId)).Returns((ExpenseCategory)null);
            TransactionService transactionService = new TransactionService(null, expenseCategoryRepositoryMock.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => transactionService.DeleteExpenseCategory(expenseCategoryId, user));
        }

        [Test]
        public void DeleteExpenseCategory_WithInvalidUser_ShouldThrowException()
        {
            // Arrange
            int expenseCategoryId = 1;
            User user = new User { Id = 1 };
            ExpenseCategory expenseCategory = new ExpenseCategory { Id = expenseCategoryId, User = new User { Id = 2 } };
            Mock<ExpenseCategoryRepository> expenseCategoryRepositoryMock = new Mock<ExpenseCategoryRepository>();
            expenseCategoryRepositoryMock.Setup(x => x.GetExpenseCategory(expenseCategoryId)).Returns(expenseCategory);
            TransactionService transactionService = new TransactionService(null, expenseCategoryRepositoryMock.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => transactionService.DeleteExpenseCategory(expenseCategoryId, user));
        }
    }
}


