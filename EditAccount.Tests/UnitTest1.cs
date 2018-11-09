using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EditAccount.Tests
{
    [TestClass]
    public class EditAccountTests
    {
        String username = "testname";
        String password = "testpass";
        String email = "testname@email.com";


        [TestMethod]
        public void Check_Username_Success()
        {
            var infoChecker = new AccountService;
            Assert.IsTrue(infoChecker.checkInfo(username, password, email));
        }

        [TestMethod]
        public void SQLWrite_Password_Success()
        {
            Assert.AreEqual("testpass", password);
        }

        [TestMethod]
        public void Check_Username_Empty_Field_Fails()
        {
            Assert.AreEqual("", username);
        }

        [TestMethod]
        public void Check_Password_Empty_Field_Fails()
        {
            Assert.AreEqual("", password);
        }

        //[TestMethod]
        //public void SQLWrite_Username_ThrowException()
        //{

        //}

        //[TestMethod]
        //public void SQLWrite_Password_ThrowException()
        //{

        //}
    }
}
