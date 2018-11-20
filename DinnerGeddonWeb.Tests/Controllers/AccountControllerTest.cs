using System;
using System.Web.Mvc;
using DinnergeddonService;
using DinnergeddonWeb.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DinnerGeddonWeb.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest

    {

        [TestMethod]
        public void Test_Register_Display_Register_View_Passes()
        {
            // Arrange
            AccountController controller = new AccountController();

            // Act
            ViewResult result = controller.Register() as ViewResult;

            // Assert
            Assert.AreEqual("You're on the register page now!", result.ViewBag.Message);

        }

        [TestMethod]
        public void Test_Login_User_Passes()
        {
            // Arrange
            AccountController controller = new AccountController();

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.AreEqual("UserView", result.ViewName);
        }

        [TestMethod]
        public void Test_Login_User_Passes()
        {
            // Arrange
            AccountController controller = new AccountController();

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.AreEqual("AdminView", result.ViewName);
        }
    }
}
