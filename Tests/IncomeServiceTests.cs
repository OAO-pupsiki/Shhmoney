using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using Shhmoney.Data;
using Shhmoney.Models;

namespace Shhmoney.Services.Tests
{


    [TestFixture]
    public class IncomeServiceTests
    {
        private IncomeRepository _incomeRepository;
        private IncomeService _incomeService;

        [SetUp]
        public void SetUp()
        {
            _incomeRepository = new IncomeRepository();
            _incomeService = new IncomeService(_incomeRepository);
        }

        [Test]
        public void AddIncome_ShouldAddIncomeToRepository()
        {
            // Arrange
            var income = new Income { Id = 1, Description = "Test Income", Amount = 100 };

            // Act
            _incomeService.AddIncome(income);

            // Assert
            var savedIncome = _incomeRepository.GetIncomeById(income.Id);
            Assert.IsNotNull(savedIncome);
            Assert.AreEqual(income.Description, savedIncome.Description);
            Assert.AreEqual(income.Amount, savedIncome.Amount);
        }

        [Test]
        public void DelIncomeByCategory_ShouldDeleteIncomesByCategory()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Salary", UserId = 1 };
            var income1 = new Income { Id = 1, Description = "Income 1", Amount = 100, Category = category };
            var income2 = new Income { Id = 2, Description = "Income 2", Amount = 200, Category = category };
            _incomeRepository.AddIncome(income1);
            _incomeRepository.AddIncome(income2);

            // Act
            _incomeService.DelIncomeByCategory(category);

            // Assert
            var incomes = _incomeRepository.GetIncomesByCategory(category);
            Assert.IsEmpty(incomes);
        }

        [Test]
        public void ViewIncomeFromCategory_ShouldReturnIncomesFromCategory()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Salary", UserId = 1 };
            var income1 = new Income { Id = 1, Description = "Income 1", Amount = 100, Category = category };
            var income2 = new Income { Id = 2, Description = "Income 2", Amount = 200, Category = category };
            _incomeRepository.AddIncome(income1);
            _incomeRepository.AddIncome(income2);

            // Act
            var result = _incomeService.ViewIncomeFromCategory(category);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.Contains(income1, result);
            Assert.Contains(income2, result);
        }

        [Test]
        public void ChangeIncome_ShouldChangeIncomeInRepository()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Salary", UserId = 1 };
            var income = new Income { Id = 1, Description = "Income", Amount = 100, Category = category };
            _incomeRepository.AddIncome(income);

            var updatedIncome = new Income { Id = 1, Description = "Updated Income", Amount = 200, Category = category };

            // Act
            _incomeService.ChangeIncome(category, updatedIncome);

            // Assert
            var savedIncome = _incomeRepository.GetIncomeById(income.Id);
            Assert.IsNotNull(savedIncome);
            Assert.AreEqual(updatedIncome.Description, savedIncome.Description);
            Assert.AreEqual(updatedIncome.Amount, savedIncome.Amount);
        }
    }


}
