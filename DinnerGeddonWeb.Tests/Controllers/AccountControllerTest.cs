using System;
using System.Web.Mvc;
using DinnerGeddonWeb.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DinnerGeddonWeb.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest

    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void ShouldDisplayRegisterView()
        {
            // Arrange
            AccountController controller = new AccountController();

            // Act
            ViewResult result = controller.Register() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("You're on the register page now!", result.ViewBag.Message);

        }

        [TestMethod]
        public void ShouldReturnTrueIfAccountRegistered()
        {
            var proxy = new AccountService();

            bool accountRegisterStatus = proxy.Register("A Nice gay", "me@me.me", "1234");

            Assert.IsTrue(accountRegisterStatus);
        }
    }
}
