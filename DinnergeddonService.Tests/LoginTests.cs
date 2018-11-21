using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DinnergeddonService.Tests
{
    [TestClass]
    public class LoginTests
    {
        string email = "testname@email.com";
        string password = "testPass1";

        [TestMethod]
        public void Test_Login_All_Correct_Info_Succeeds()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            Assert.IsTrue(infoChecker.Login(email, password));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Login_Incorrect_Email_ThrowsException()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            infoChecker.Login("wrongemail", password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Login_Incorrect_Password_ThrowsException()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            infoChecker.Login(email, "wrongpassword");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Login_Blank_Email_Field_ThrowsException()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            infoChecker.Login(" ", password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Login_Blank_Password_Field_ThrowsException()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            infoChecker.Login(email, " ");
        }
    }
}
