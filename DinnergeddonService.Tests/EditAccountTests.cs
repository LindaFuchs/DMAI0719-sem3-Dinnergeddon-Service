using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DinnergeddonService.Tests
{
    [TestClass]
    public class EditAccountTests
    {
        String username = "testname";
        String password = "testpass";
        String email = "testname@email.com";


        [TestMethod]
        public void CheckInfo_All_Correct_Info_Succeeds()
        {
            var infoChecker = new AccountService();
            Assert.IsTrue(infoChecker.EditAccount(username, password, email));
        }

        [TestMethod]
        public void CheckInfo_Blank_Username_Fails()
        {
            var infoChecker = new AccountService();
            Assert.IsFalse(infoChecker.EditAccount("", password, email));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckInfo_Blank_Password_Fails()
        {
            var infoChecker = new AccountService();
            Assert.IsFalse(infoChecker.EditAccount(username, "", email));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckInfo_Blank_Email_Fails()
        {
            var infoChecker = new AccountService();
            Assert.IsFalse(infoChecker.EditAccount(username, password, ""));
        }

        [TestMethod]
        public void Test_Something()
        {

        }
    }
}
