using Moq;
using NUnit.Framework;
using Shhmoney.Data;
using Shhmoney.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shhmoney.Services.Tests
{
    [TestFixture]
    public class LimitServiceTests
    {
        private Mock<LimitRepository> _limitRepositoryMock;
        private LimitService _limitService;

        [SetUp]
        public void SetUp()
        {
            _limitRepositoryMock = new Mock<LimitRepository>();
            _limitService = new LimitService(_limitRepositoryMock.Object);
        }

        [Test]
        public void Add_ValidParameters_ReturnsMounthLimit()
        {
            // Arrange
            var categoryId = 1;
            var currency = "USD";
            var limit = 100;
            var expectedMounthLimit = new MounthLimit
            {
                UserId = 1,
                ExpenseCategoryId = categoryId,
                Currency = currency,
                Limit = limit,
            };
            _limitRepositoryMock.Setup(r => r.Add(expectedMounthLimit)).Returns(expectedMounthLimit);

            // Act
            var result = _limitService.Add(categoryId, currency, limit);

            // Assert
            Assert.AreEqual(expectedMounthLimit, result);
            _limitRepositoryMock.Verify(r => r.Add(expectedMounthLimit), Times.Once);
        }

        [Test]
        public void GetMounthLimitById_ExistingCategoryId_ReturnsMounthLimit()
        {
            // Arrange
            var categoryId = 1;
            var expectedMounthLimit = new MounthLimit
            {
                UserId = 1,
                ExpenseCategoryId = categoryId,
                Currency = "USD",
                Limit = 100,
            };
            _limitRepositoryMock.Setup(r => r.GetMounthLimitById(categoryId)).Returns(expectedMounthLimit);

            // Act
            var result = _limitService.GetMounthLimitById(categoryId);

            // Assert
            Assert.AreEqual(expectedMounthLimit, result);
            _limitRepositoryMock.Verify(r => r.GetMounthLimitById(categoryId), Times.Once);
        }
    }

}
