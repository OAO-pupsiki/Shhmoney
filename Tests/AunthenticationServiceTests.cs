
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using Shhmoney.Data;
using Moq;
using Shhmoney.Models;
using Shhmoney.Utils;

namespace Shhmoney.Services.Tests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private UserRepository? _userRepository;
        private RoleRepository? _roleRepository;
        private UserSessionRepository? _userSessionRepository;
        private AuthenticationService? _authenticationService;

        [SetUp]
        public void SetUp()
        {
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
            _userSessionRepository = new UserSessionRepository();
            _authenticationService = new AuthenticationService(_userRepository, _roleRepository, _userSessionRepository);
        }

        [Test]
        public void Login_ValidCredentials_ShouldSetCurrentUser()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";
            var rememberMe = false;
            var role = new Role { Name = "User", Description = "Simple user role" };
            var user = new User { Username = username, Password = PasswordHasher.HashPassword(password), Email = "testuser@example.com", Role = role };
            _userRepository.AddUser(user);

            // Act
            _authenticationService.Login(username, password, rememberMe);

            // Assert
            Assert.AreEqual(user, Utils.AppContext.CurrentUser);
        }

        [Test]
        public void Login_InvalidUsername_ShouldThrowException()
        {
            // Arrange
            var username = "nonexistentuser";
            var password = "testpassword";
            var rememberMe = false;

            // Act and Assert
            Assert.Throws<Exception>(() => _authenticationService.Login(username, password, rememberMe));
        }

        [Test]
        public void Login_InvalidPassword_ShouldThrowException()
        {
            // Arrange
            var username = "testuser";
            var password = "wrongpassword";
            var rememberMe = false;
            var role = new Role { Name = "User", Description = "Simple user role" };
            var user = new User { Username = username, Password = PasswordHasher.HashPassword("testpassword"), Email = "testuser@example.com", Role = role };
            _userRepository.AddUser(user);

            // Act and Assert
             Assert.Throws<Exception>(() => _authenticationService.Login(username, password, rememberMe));
        }

        [Test]
        public void LogOut_WithValidUser_UpdatesSessionExpiration()
        {
            // Arrange
            var userRepository = new Mock<UserRepository>();
            var roleRepository = new Mock<RoleRepository>();
            var userSessionRepository = new Mock<UserSessionRepository>();

            var user = new User
            {
                Id = 1,
                Username = "john_doe",
                Password = PasswordHasher.HashPassword("P@ssw0rd"),
                Email = "john.doe@example.com",
                Role = new Role
                {
                    Id = 1,
                    Name = "User",
                    Description = "Simple user role"
                }
            };

            var session = new UserSession
            {
                Token = "token",
                Expiration = DateTime.UtcNow.AddHours(1),
                User = user
            };

            userSessionRepository.Setup(x => x.GetSessionByUser(user))
                .Returns(session);

            var authService = new AuthenticationService(
                userRepository.Object,
                roleRepository.Object,
                userSessionRepository.Object);

            // Act
            authService.LogOut(user);

            // Assert
            Assert.AreEqual(DateTime.UtcNow, session.Expiration);
        }

        [Test]
        public void TryAutoLogin_WithValidToken_ReturnsTrueAndSetsCurrentUser()
        {
            // Arrange
            var userRepository = new Mock<UserRepository>();
            var roleRepository = new Mock<RoleRepository>();
            var userSessionRepository = new Mock<UserSessionRepository>();

            var user = new User
            {
                Id = 1,
                Username = "john_doe",
                Password = PasswordHasher.HashPassword("P@ssw0rd"),
                Email = "john.doe@example.com",
                Role = new Role
                {
                    Id = 1,
                    Name = "User",
                    Description = "Simple user role"
                }
            };

            var session = new UserSession
            {
                Token = "token",
                Expiration = DateTime.UtcNow.AddHours(1),
                User = user
            };

            userSessionRepository.Setup(x => x.GetSessionByToken("token"))
                .Returns(session);

            userRepository.Setup(x => x.GetUserById(1))
                .Returns(user);

            var authService = new AuthenticationService(
                userRepository.Object,
                roleRepository.Object,
                userSessionRepository.Object);

            // Act
            bool result = authService.TryAutoLogin();

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(user, Utils.AppContext.CurrentUser);
        }

        [Test]
        public void TryAutoLogin_WithExpiredSession_ReturnsFalse()
        {
            // Arrange
            var userRepository = new Mock<UserRepository>();
            var roleRepository = new Mock<RoleRepository>();
            var userSessionRepository = new Mock<UserSessionRepository>();

            var session = new UserSession
            {
                Token = "token",
                Expiration = DateTime.UtcNow.AddHours(-1),
                User = new User
                {
                    Id = 1,
                    Username = "john_doe",
                    Password = PasswordHasher.HashPassword("P@ssw0rd"),
                    Email = "john.doe@example.com",
                    Role = new Role
                    {
                        Id = 1,
                        Name = "User",
                        Description = "Simple user role"
                    }
                }
            };

            userSessionRepository.Setup(x => x.GetSessionByToken("token"))
                .Returns(session);

            var authService = new AuthenticationService(
                userRepository.Object,
                roleRepository.Object,
                userSessionRepository.Object);

            // Act
            bool result = authService.TryAutoLogin();

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(Utils.AppContext.CurrentUser);
        }


        [Test]
        public void SignUp_Should_Add_User_To_Repository()
        {
            // Arrange
            var userRepository = new UserRepository();
            var roleRepository = new RoleRepository();
            var userSessionRepository = new UserSessionRepository();
            var authService = new AuthenticationService(userRepository, roleRepository, userSessionRepository);
            var username = "testuser";
            var password = "testpassword";
            var email = "testuser@test.com";

            // Act
            var result = authService.SignUp(username, password, email);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(userRepository.GetUserByUsername(username));
        }

        [Test]
        public void SignUp_Should_Throw_Exception_If_Username_Already_Taken()
        {
            // Arrange
            var userRepository = new UserRepository();
            var roleRepository = new RoleRepository();
            var userSessionRepository = new UserSessionRepository();
            var authService = new AuthenticationService(userRepository, roleRepository, userSessionRepository);
            var username = "testuser";
            var password = "testpassword";
            var email = "testuser@test.com";
            authService.SignUp(username, password, email);

            // Act & Assert
            Assert.Throws<Exception>(() => authService.SignUp(username, password, email));
        }
    }

}

    

