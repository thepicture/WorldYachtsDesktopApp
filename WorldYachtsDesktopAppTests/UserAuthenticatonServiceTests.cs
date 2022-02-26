using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using WorldYachtsDesktopApp;
using WorldYachtsDesktopApp.Models.LoginModels;
using WorldYachtsDesktopApp.Services;

namespace WorldYachtsDesktopAppTests
{
    [TestClass]
    public class UserAuthenticatonServiceTests
    {
        private static IAuthenticationService service;
#pragma warning disable IDE0052 // Remove unread private members
        private static App app;
#pragma warning restore IDE0052 // Remove unread private members

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            app = new App();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            app = null;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            service = new UserAuthenticatonService(owner: null,
                                                    blocker: null,
                                                    repository: new StubUserRepository());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            service = null;
        }

        [TestMethod]
        public async Task LoginAsync_SuccessfulLogin_ReturnsOkLoginResponse()
        {
            // Arrange.
            Type expected = typeof(OkLoginResponse);
            string login = "123";
            string password = "321";
            // Act.
            ILoginResponse actual = await service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }

        [TestMethod]
        public async Task LoginAsync_IncorrectLoginAndPassword_ReturnsIncorrectLoginResponse()
        {
            // Arrange.
            Type expected = typeof(IncorrectLoginResponse);
            string login = "___";
            string password = "___";
            // Act.
            ILoginResponse actual = await service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }

        [TestMethod]
        public async Task LoginAsync_CorrectLoginIncorrectPassword_ReturnsIncorrectLoginResponse()
        {
            // Arrange.
            Type expected = typeof(IncorrectLoginResponse);
            string login = "122";
            string password = "___";
            // Act.
            ILoginResponse actual = await service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }

        [TestMethod]
        public async Task LoginAsync_IncorrectLoginCorrectPassword_ReturnsIncorrectLoginResponse()
        {
            // Arrange.
            Type expected = typeof(IncorrectLoginResponse);
            string login = "___";
            string password = "321";
            // Act.
            ILoginResponse actual = await service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }

        [TestMethod]
        public void LoginAsync_NoAnyActions_BlockTimeIsZeroSeconds()
        {
            // Arrange.
            TimeSpan expected = TimeSpan.Zero;
            // Act.
            TimeSpan actual = service.BlockTime;
            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task LoginAsync_SuccessfulAuthentication_BlockTimeIsZeroSeconds()
        {
            // Arrange.
            TimeSpan expected = TimeSpan.Zero;
            string login = "123";
            string password = "321";
            // Act.
            _ = await service.LoginAsync(login, password);
            TimeSpan actual = service.BlockTime;
            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task LoginAsync_ThreeTimesIncorrectAuthentication_BlockTimeIs15Seconds()
        {
            // Arrange.
            TimeSpan expected = TimeSpan.FromSeconds(15);
            string login = "___";
            string password = "___";
            // Act.
            for (int i = 0; i < 3; i++)
            {
                _ = await service.LoginAsync(login, password);
            }
            TimeSpan actual = service.BlockTime;
            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task LoginAsync_FourTimesIncorrectAuthentication_BlockTimeIs35Seconds()
        {
            // Arrange.
            TimeSpan expected = TimeSpan.FromSeconds(35);
            string login = "___";
            string password = "___";
            // Act.
            for (int i = 0; i < 4; i++)
            {
                _ = await service.LoginAsync(login, password);
            }
            TimeSpan actual = service.BlockTime;
            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task LoginAsync_FourTimesIncorrectAuthenticationThenCorrect_BlockTimeIsZeroSeconds()
        {
            // Arrange.
            TimeSpan expected = TimeSpan.FromSeconds(0);
            string login = "___";
            string password = "___";
            // Act.
            for (int i = 0; i < 4; i++)
            {
                _ = await service.LoginAsync(login, password);
            }
            login = "123";
            password = "321";
            _ = await service.LoginAsync(login, password);
            TimeSpan actual = service.BlockTime;
            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task LoginAsync_LoginToOneMonthAccount_ReturnsBlockedLoginResponse()
        {
            // Arrange.
            Type expected = typeof(BlockedLoginResponse);
            string login = "xyz";
            string password = "zyx";
            // Act.
            ILoginResponse actual = await service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }

        [TestMethod]
        public async Task LoginAsync_LoginCaseDoesNotMatch_ReturnsOkLoginResponse()
        {
            // Arrange.
            Type expected = typeof(OkLoginResponse);
            string login = "CdE";
            string password = "123";
            // Act.
            ILoginResponse actual = await service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }

        [TestMethod]
        public async Task LoginAsync_LoginTo14PasswordUnchangedAccount_ReturnsOkButChangePasswordResponse()
        {
            // Arrange.
            Type expected = typeof(OkButChangePasswordResponse);
            string login = "abc";
            string password = "cba";
            // Act.
            ILoginResponse actual = await service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }

        [TestMethod]
        public async Task LoginAsync_PasswordCaseDoesNotMatch_ReturnsIncorrectLoginResponse()
        {
            // Arrange.
            Type expected = typeof(IncorrectLoginResponse);
            string login = "abc";
            string password = "cBa";
            // Act.
            ILoginResponse actual = await service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }
    }
}
