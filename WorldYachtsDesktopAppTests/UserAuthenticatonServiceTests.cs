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
        private IAuthenticationService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _service = new UserAuthenticatonService();
        }

        [TestMethod]
        public async Task LoginAsync_SuccessfulLogin_ReasonIsOk()
        {
            // Arrange.
            Type expected = typeof(OkLoginResponse);
            string login = "123";
            string password = "321";
            // Act.
            ILoginResponse actual = await _service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }

        [TestMethod]
        public async Task LoginAsync_IncorrectLoginAndPassword_ReasonIsIncorrect()
        {
            // Arrange.
            Type expected = typeof(IncorrectLoginResponse);
            string login = "___";
            string password = "___";
            // Act.
            ILoginResponse actual = await _service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }

        [TestMethod]
        public async Task LoginAsync_CorrectLoginIncorrectPassword_ReasonIsIncorrect()
        {
            // Arrange.
            Type expected = typeof(IncorrectLoginResponse);
            string login = "122";
            string password = "___";
            // Act.
            ILoginResponse actual = await _service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }

        [TestMethod]
        public async Task LoginAsync_IncorrectLoginCorrectPassword_ReasonIsIncorrect()
        {
            // Arrange.
            Type expected = typeof(IncorrectLoginResponse);
            string login = "___";
            string password = "321";
            // Act.
            ILoginResponse actual = await _service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }

        [TestMethod]
        public void LoginAsync_NoAnyActions_BlockTimeIsZeroSeconds()
        {
            // Arrange.
            TimeSpan expected = TimeSpan.Zero;
            // Act.
            TimeSpan actual = _service.BlockTime;
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
            _ = await _service.LoginAsync(login, password);
            TimeSpan actual = _service.BlockTime;
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
                _ = await _service.LoginAsync(login, password);
            }
            TimeSpan actual = _service.BlockTime;
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
                _ = await _service.LoginAsync(login, password);
            }
            TimeSpan actual = _service.BlockTime;
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
                _ = await _service.LoginAsync(login, password);
            }
            login = "123";
            password = "321";
            _ = await _service.LoginAsync(login, password);
            TimeSpan actual = _service.BlockTime;
            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task LoginAsync_LoginToOneMonthAccount_AccountIsBlocked()
        {
            // Arrange.
            Type expected = typeof(BlockedLoginResponse);
            string login = "xyz";
            string password = "zyx";
            // Act.
            ILoginResponse actual = await _service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }

        [TestMethod]
        public async Task LoginAsync_LoginCaseDoesNotMatch_ReasonIsOk()
        {
            // Arrange.
            Type expected = typeof(OkLoginResponse);
            string login = "CdE";
            string password = "123";
            // Act.
            ILoginResponse actual = await _service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }

        [TestMethod]
        public async Task LoginAsync_LoginCaseDoesNotMatch_ReasonOkButChangePasswordResponse()
        {
            // Arrange.
            Type expected = typeof(OkButChangePasswordResponse);
            string login = "abc";
            string password = "cba";
            // Act.
            ILoginResponse actual = await _service.LoginAsync(login, password);
            // Assert.
            Assert.IsInstanceOfType(actual, expected);
        }
    }
}
