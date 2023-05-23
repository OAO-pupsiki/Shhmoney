
using Moq;
using NUnit.Framework;
using Shhmoney.Data;
using Shhmoney.Models;
using Assert = NUnit.Framework.Assert;

namespace Shhmoney.Services.Tests
{


    [TestFixture]
    public class AccountServiceTests
    {
        [Test]
        public void AddAccount_Should_Return_Newly_Created_Account()
        {
            // Arrange
            var accountRepository = new Mock<AccountRepository>();
            var accountService = new AccountService(accountRepository.Object);
            var account = new Account { Id = 1, Name = "Test Account", Balance = 100 };

            accountRepository.Setup(x => x.AddAccount(account))
                .Returns(account);

            // Act
            var result = accountService.AddAccount(account);

            // Assert
            Assert.AreEqual(account, result);
        }

        [Test]
        public void GetAccountById_Should_Return_Correct_Account()
        {
            // Arrange
            var accountRepository = new Mock<AccountRepository>();
            var accountService = new AccountService(accountRepository.Object);
            var account = new Account { Id = 1, Name = "Test Account", Balance = 100 };

            accountRepository.Setup(x => x.GetAccountById(1))
                .Returns(account);

            // Act
            var result = accountService.GetAccountById(1);

            // Assert
            Assert.AreEqual(account, result);
        }

        [Test]
        public void GetAllAccounts_Should_Return_All_Accounts()
        {
            // Arrange
            var accountRepository = new Mock<AccountRepository>();
            var accountService = new AccountService(accountRepository.Object);
            var accounts = new List<Account>
        {
            new Account { Id = 1, Name = "Account 1", Balance = 100 },
            new Account { Id = 2, Name = "Account 2", Balance = 200 },
            new Account { Id = 3, Name = "Account 3", Balance = 300 }
        };

            accountRepository.Setup(x => x.GetAllAccounts())
                .Returns(accounts);

            // Act
            var result = accountService.GetAllAccounts();

            // Assert
            Assert.AreEqual(accounts, result);
        }
    }


}