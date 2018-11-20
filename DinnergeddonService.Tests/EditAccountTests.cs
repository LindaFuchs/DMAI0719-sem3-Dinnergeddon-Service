using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DinnergeddonService;
using DBLayer;
using System.Data;
using System.Data.Common;
using Model;

namespace DinnergeddonService.Tests
{
    [TestClass]
    public class EditAccountTests
    {
        String username = "userName1";
        String email = "testname@email.com";
        String password = "testPass1";


        [TestMethod]
        public void Test_EditAccount_All_Correct_Info_Succeeds()
        {
            var infoChecker = new AccountServiceMock();
            Assert.IsTrue(infoChecker.EditAccount(username, email, password));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_EditAccount_Blank_Username_Throws_Exception()
        {
            var infoChecker = new AccountServiceMock();
            infoChecker.EditAccount("", email, password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_EditAccount_Blank_Email_Throws_Exception()
        {
            var infoChecker = new AccountServiceMock();
            infoChecker.EditAccount(username, "", password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_EditAccount_Blank_Password_Throws_Exception()
        {
            var infoChecker = new AccountServiceMock();
            infoChecker.EditAccount(username, email, "");
        }

        [TestMethod]
        public void Test_CheckEmail_Correct_Info_Passes()
        {
            var infoChecker = new AccountServiceMock();
            Assert.IsTrue(infoChecker.CheckEmail(email));
        }

        [TestMethod]
        public void Test_CheckEmail_Invalid_Format_Fails()
        {
            var infoChecker = new AccountServiceMock();
            Assert.IsTrue(infoChecker.CheckEmail("john@mail"));
        }

        [TestMethod]
        public void Test_CheckEmail_Blank_Fails()
        {
            var infoChecker = new AccountServiceMock();
            Assert.IsFalse(infoChecker.CheckEmail("  "));
        }

        [TestMethod]
        public void Test_CheckUsername_Correct_Info_Passes()
        {
            var infoChecker = new AccountServiceMock();
            Assert.IsTrue(infoChecker.CheckUsername(username));
        }

        [TestMethod]
        public void Test_CheckUsername_Characters_Over_Limit_Fails()
        {
            //32 char limit
            var infoChecker = new AccountServiceMock();
            Assert.IsFalse(infoChecker.CheckUsername("123456789012345678901234567890123"));
        }

        [TestMethod]
        public void Test_CheckUsername_Characters_Under_Limit_Fails()
        {
            var infoChecker = new AccountServiceMock();
            Assert.IsFalse(infoChecker.CheckUsername("us"));
        }

        [TestMethod]
        public void Test_CheckUsername_Characters_Not_Permitted_Fails()
        {
            var infoChecker = new AccountServiceMock();
            Assert.IsFalse(infoChecker.CheckUsername("ÆÆ"));
        }

        [TestMethod]
        public void Test_CheckUsername_Blank_Fails()
        {
            var infoChecker = new AccountServiceMock();
            Assert.IsFalse(infoChecker.CheckUsername("  "));
        }

        [TestMethod]
        public void Test_CheckPassword_CorrectInfo_Passes()
        {
            var infoChecker = new AccountServiceMock();
            Assert.IsTrue(infoChecker.CheckPassword(password));
        }

        [TestMethod]
        public void Test_CheckPassword_Character_Over_Limit_Fails()
        {
            var infoChecker = new AccountServiceMock();
            Assert.IsFalse(infoChecker.CheckPassword("1234567890123456789012345678901"));
        }

        [TestMethod]
        public void Test_CheckPassword_Character_Under_Limit_Fails()
        {
            var infoChecker = new AccountServiceMock();
            Assert.IsFalse(infoChecker.CheckPassword("ss"));
        }

        [TestMethod]
        public void Test_CheckPassword_Characters_Not_Permitted_Fails()
        {
            var infoChecker = new AccountServiceMock();
            Assert.IsFalse(infoChecker.CheckPassword("ÆÆÆ"));
        }
    }

    public class AccountServiceMock : IAccountService
    {
        public bool CheckEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool CheckPassword(string password)
        {
            throw new NotImplementedException();
        }

        public bool CheckUsername(string username)
        {
            throw new NotImplementedException();
        }

        public bool EditAccount(string username, string email, string password)
        {
            throw new NotImplementedException();
        }

        public Account FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Account FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Account GetInfo()
        {
            throw new NotImplementedException();
        }

        public bool InsertAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public bool Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool RegisterAccount(string username, string email, string password)
        {
            throw new NotImplementedException();
        }
    }

    public class DbComponentsMock : IDbComponents
    {
        public IDbConnection Connection => throw new NotImplementedException();

        public DbProviderFactory Factory => throw new NotImplementedException();
    }
}
