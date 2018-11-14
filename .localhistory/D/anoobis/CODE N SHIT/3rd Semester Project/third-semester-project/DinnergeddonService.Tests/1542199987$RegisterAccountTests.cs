using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DinnergeddonService.Tests
{
    [TestClass]
    public class RegisterAccountTests
    {
        [TestMethod]
        public void ShouldReturnTrueIfAccountRegistered()
        {
            // Arrange
            var proxy = new AccountService();

            // Act
            bool accountRegisterStatus = proxy.Register("A Nice gay", "me@me.me", "1234");

            // Assert
            Assert.IsTrue(accountRegisterStatus);
        }

        [TestMethod]
        public void ShouldReturnFalseWhenUsernameTaken()
        {
            // Arrange
            var proxy = new AccountService();
            // Do we need this to be in the DB before the test?
            // proxy.Register("Ivan");
            string newUserName = "Ivan";
            // Act
            bool checkUserName = proxy.CheckUsername(newUserName);
            // Assert
            Assert.IsFalse(checkUserName);
                

        }
        [TestMethod]
        public void ShouldReturnTrueWhenUsernameNotTaken()
        {
            // Arrange
            var proxy = new AccountService();
            string newUserName = "Ivan";
            // Act
            bool checkUserName = proxy.CheckUsername(newUserName);
            // Assert
            Assert.IsTrue(checkUserName);

        }

        [TestMethod]
        public void ShouldReturnTrueWhenEmailNotTaken()
        {
            // Arrange
            var proxy = new AccountService();
            string newEmail = "me@me.me";
            // Act
            bool checkEmail = proxy.CheckEmail(newEmail);
            // Assert
            Assert.IsTrue(checkEmail);

        }
        [TestMethod]
        public void ShouldReturnTrueWhenEmailTaken()
        {
            // Arrange
            var proxy = new AccountService();
            // Do we need this to be in the DB before the test?
            // proxy.Register("me@me.me");
            string newEmail = "me@me.me";
            // Act
            bool checkEmail = proxy.CheckEmail(newEmail);
            // Assert
            Assert.IsFalse(checkEmail);

        }


    }
}
