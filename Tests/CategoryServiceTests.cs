
using Moq;
using NUnit.Framework;
using Shhmoney.Data;
using Shhmoney.Models;
using Shhmoney.Services;

namespace Tests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private CategoryService _categoryService;
        private Mock<IncomeCategoryRepository> _incomeCategoryRepositoryMock;
        private Mock<ExpenseCategoryRepository> _expenseCategoryRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _incomeCategoryRepositoryMock = new Mock<IncomeCategoryRepository>();
            _expenseCategoryRepositoryMock = new Mock<ExpenseCategoryRepository>();

            _categoryService = new CategoryService(
                _incomeCategoryRepositoryMock.Object,
                _expenseCategoryRepositoryMock.Object
            );
        }

        [Test]
        public void AddExpenseCategory_ShouldCallRepository()
        {
            // Arrange
            var category = new ExpenseCategory
            {
                Name = "Groceries",
                Description = "Expenses related to food"
            };

            // Act
            _categoryService.AddExpenseCategory(category);

            // Assert
            _expenseCategoryRepositoryMock.Verify(x => x.AddExpenseCategory(category), Times.Once);
        }

        [Test]
        public void AddIncomeCategory_ShouldCallRepository()
        {
            // Arrange
            var category = new IncomeCategory
            {
                Name = "Salary",
                Description = "Income from work"
            };

            // Act
            _categoryService.AddIncomeCategory(category);

            // Assert
            _incomeCategoryRepositoryMock.Verify(x => x.AddIncomeCategory(category), Times.Once);
        }
    }


}