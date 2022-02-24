using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
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
            LoginReason expected = LoginReason.Ok;
            string login = "123";
            string password = "321";
            // Act.
            await _service.LoginAsync(login, password);
            LoginReason actual = _service.GetReason();
            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task LoginAsync_IncorrectLoginAndPassword_ReasonIsIncorrect()
        {
            // Arrange.
            LoginReason expected = LoginReason.Incorrect;
            string login = "___";
            string password = "___";
            // Act.
            await _service.LoginAsync(login, password);
            LoginReason actual = _service.GetReason();
            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task LoginAsync_CorrectLoginIncorrectPassword_ReasonIsIncorrect()
        {
            // Arrange.
            LoginReason expected = LoginReason.Incorrect;
            string login = "122";
            string password = "___";
            // Act.
            await _service.LoginAsync(login, password);
            LoginReason actual = _service.GetReason();
            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task LoginAsync_IncorrectLoginCorrectPassword_ReasonIsIncorrect()
        {
            // Arrange.
            LoginReason expected = LoginReason.Incorrect;
            string login = "___";
            string password = "321";
            // Act.
            await _service.LoginAsync(login, password);
            LoginReason actual = _service.GetReason();
            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoginAsync_NoAnyActions_ReasonIsNoActions()
        {
            // Arrange.
            LoginReason expected = LoginReason.NoActions;
            // Act.
            LoginReason actual = _service.GetReason();
            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoginAsync_NoAnyActions_BlockTimeIsZeroSeconds()
        {
            // Arrange.
            TimeSpan expected = TimeSpan.Zero;
            // Act.
            TimeSpan actual = _service.GetBlockTime();
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
            await _service.LoginAsync(login, password);
            TimeSpan actual = _service.GetBlockTime();
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
                await _service.LoginAsync(login, password);
            }
            TimeSpan actual = _service.GetBlockTime();
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
                await _service.LoginAsync(login, password);
            }
            TimeSpan actual = _service.GetBlockTime();
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
                await _service.LoginAsync(login, password);
            }
            login = "123";
            password = "321";
            await _service.LoginAsync(login, password);
            TimeSpan actual = _service.GetBlockTime();
            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task LoginAsync_LoginToOneMonthAccount_AccountIsBlocked()
        {
            // Arrange.
            LoginReason expected = LoginReason.IsBlocked;
            string login = "xyz";
            string password = "zyx";
            // Act.
            await _service.LoginAsync(login, password);
            LoginReason actual = _service.GetReason();
            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task LoginAsync_LoginCaseDoesNotMatch_ReasonIsOk()
        {
            // Arrange.
            LoginReason expected = LoginReason.Ok;
            string login = "CdE";
            string password = "123";
            // Act.
            await _service.LoginAsync(login, password);
            LoginReason actual = _service.GetReason();
            // Assert.
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task LoginAsync_LoginCaseDoesNotMatch_ReasonIsNeedToChangePasswordButOk()
        {
            // Arrange.
            LoginReason expected = LoginReason.NeedToChangePasswordButOk;
            string login = "abc";
            string password = "cba";
            // Act.
            await _service.LoginAsync(login, password);
            LoginReason actual = _service.GetReason();
            // Assert.
            Assert.AreEqual(expected, actual);
        }
    }
}
