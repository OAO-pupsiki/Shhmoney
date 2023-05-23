using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Shhmoney.Data;
using Shhmoney.Models;
using Shhmoney.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace Tests
{
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class AuthorizationServiceTests
    {
        private Mock<RoleRepository> _roleRepoMock;
        private AuthorizationService _authorizationService;

        [SetUp]
        public void Setup()
        {
            _roleRepoMock = new Mock<RoleRepository>();
            _authorizationService = new AuthorizationService(_roleRepoMock.Object);
        }

        [Test]
        public void IsUserInRole_WithValidUserRole_ReturnsTrue()
        {
            // Arrange
            var user = new User { RoleId = 1 };
            var roleName = "User";
            var role = new Role { Id = 1, Name = roleName, Description = "User Role" };

            _roleRepoMock.Setup(repo => repo.GetRoleByName(roleName)).Returns(role);
            _roleRepoMock.Setup(repo => repo.GetRoleById(user.RoleId)).Returns(role);

            // Act
            var result = _authorizationService.IsUserInRole(user, roleName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsUserInRole_WithInvalidUserRole_ReturnsFalse()
        {
            // Arrange
            var user = new User { RoleId = 1 };
            var roleName = "Admin";
            var role = new Role { Id = 2, Name = roleName, Description = "Admin Role" };

            _roleRepoMock.Setup(repo => repo.GetRoleByName(roleName)).Returns(role);
            _roleRepoMock.Setup(repo => repo.GetRoleById(user.RoleId)).Returns(role);

            // Act
            var result = _authorizationService.IsUserInRole(user, roleName);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsUserInRole_WithNonexistentRole_ReturnsFalse()
        {
            // Arrange
            var user = new User { RoleId = 1 };
            var roleName = "Guest";

            _roleRepoMock.Setup(repo => repo.GetRoleByName(roleName)).Returns((Role)null);

            // Act
            var result = _authorizationService.IsUserInRole(user, roleName);

            // Assert
            Assert.IsFalse(result);
        }
    }
}