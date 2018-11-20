using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DinnergeddonService;
using DBLayer;
using System.Data;
using System.Data.Common;
using Model;
using System.Collections.Generic;

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
            var infoChecker = new AccountService(new AccountRepoMock());
            Assert.IsTrue(infoChecker.EditAccount(username, email, password));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_EditAccount_Blank_Username_Throws_Exception()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            infoChecker.EditAccount("", email, password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_EditAccount_Blank_Email_Throws_Exception()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            infoChecker.EditAccount(username, "", password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_EditAccount_Blank_Password_Throws_Exception()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            infoChecker.EditAccount(username, email, "");
        }

        [TestMethod]
        public void Test_CheckEmail_Correct_Info_Passes()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            Assert.IsTrue(infoChecker.CheckEmail(email));
        }

        [TestMethod]
        public void Test_CheckEmail_Invalid_Format_Fails()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            Assert.IsTrue(infoChecker.CheckEmail("john@mail"));
        }

        [TestMethod]
        public void Test_CheckEmail_Blank_Fails()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            Assert.IsFalse(infoChecker.CheckEmail("  "));
        }

        [TestMethod]
        public void Test_CheckUsername_Correct_Info_Passes()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            Assert.IsTrue(infoChecker.CheckUsername(username));
        }

        [TestMethod]
        public void Test_CheckUsername_Characters_Over_Limit_Fails()
        {
            //32 char limit
            var infoChecker = new AccountService(new AccountRepoMock());
            Assert.IsFalse(infoChecker.CheckUsername("123456789012345678901234567890123"));
        }

        [TestMethod]
        public void Test_CheckUsername_Characters_Under_Limit_Fails()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            Assert.IsFalse(infoChecker.CheckUsername("us"));
        }

        [TestMethod]
        public void Test_CheckUsername_Characters_Not_Permitted_Fails()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            Assert.IsFalse(infoChecker.CheckUsername("ÆÆ"));
        }

        [TestMethod]
        public void Test_CheckUsername_Blank_Fails()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            Assert.IsFalse(infoChecker.CheckUsername("  "));
        }

        [TestMethod]
        public void Test_CheckPassword_CorrectInfo_Passes()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            Assert.IsTrue(infoChecker.CheckPassword(password));
        }

        [TestMethod]
        public void Test_CheckPassword_Character_Over_Limit_Fails()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            Assert.IsFalse(infoChecker.CheckPassword("1234567890123456789012345678901"));
        }

        [TestMethod]
        public void Test_CheckPassword_Character_Under_Limit_Fails()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            Assert.IsFalse(infoChecker.CheckPassword("ss"));
        }

        [TestMethod]
        public void Test_CheckPassword_Characters_Not_Permitted_Fails()
        {
            var infoChecker = new AccountService(new AccountRepoMock());
            Assert.IsFalse(infoChecker.CheckPassword("ÆÆÆ"));
        }
    }

    public class AccountRepoMock : IAccountRepo
    {
        public int AddToRole(Guid accountID, string roleName)
        {
            throw new NotImplementedException();
        }

        public int DeleteAccount(Account a)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountByID(Guid accountID)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAccounts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetRoles(Guid accountID)
        {
            throw new NotImplementedException();
        }

        public int InsertAccount(Account a)
        {
            throw new NotImplementedException();
        }

        public bool IsInsideRole(Guid accountID, string roleName)
        {
            throw new NotImplementedException();
        }

        public int UpdateAccount(Account oldAccount, Account newAccount)
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
