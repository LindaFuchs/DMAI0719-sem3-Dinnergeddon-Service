﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DinnergeddonService;

namespace DinnergeddonService.Tests
{
    [TestClass]
    public class EditAccountTests
    {
        String username = "us-er.name_1234";
        String email = "testname@email.com";
        String password = "testPass1@#$%";


        [TestMethod]
        public void Test_EditAccount_All_Correct_Info_Succeeds()
        {
            var infoChecker = new AccountService();
            Assert.IsTrue(infoChecker.EditAccount(username, email, password));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_EditAccount_Blank_Username_Throws_Exception()
        {
            var infoChecker = new AccountService();
            infoChecker.EditAccount("", email, password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_EditAccount_Blank_Email_Throws_Exception()
        {
            var infoChecker = new AccountService();
            infoChecker.EditAccount(username, "", password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_EditAccount_Blank_Password_Throws_Exception()
        {
            var infoChecker = new AccountService();
            infoChecker.EditAccount(username, email, "");
        }

        [TestMethod]
        public void Test_CheckEmail_Correct_Info_Passes()
        {
            var infoChecker = new AccountService();
            Assert.IsTrue(infoChecker.CheckEmail(email));
        }

        [TestMethod]
        public void Test_CheckEmail_Invalid_Format_Fails()
        {
            var infoChecker = new AccountService();
            Assert.IsTrue(infoChecker.CheckEmail("john@mail"));
        }

        [TestMethod]
        public void Test_CheckEmail_Blank_Fails()
        {
            var infoChecker = new AccountService();
            Assert.IsFalse(infoChecker.CheckEmail("  "));
        }

        [TestMethod]
        public void Test_CheckUsername_Correct_Info_Passes()
        {
            var infoChecker = new AccountService();
            Assert.IsTrue(infoChecker.CheckUsername(username));
        }

        [TestMethod]
        public void Test_CheckUsername_Characters_Over_Limit_Fails()
        {
            //32 char limit
            var infoChecker = new AccountService();
            Assert.IsFalse(infoChecker.CheckUsername("123456789012345678901234567890123"));
        }

        [TestMethod]
        public void Test_CheckUsername_Characters_Under_Limit_Fails()
        {
            var infoChecker = new AccountService();
            Assert.IsFalse(infoChecker.CheckUsername("us"));
        }

        [TestMethod]
        public void Test_CheckUsername_Characters_Not_Permitted_Fails()
        {
            var infoChecker = new AccountService();
            Assert.IsFalse(infoChecker.CheckUsername("ÆÆ"));
        }

        [TestMethod]
        public void Test_CheckUsername_Blank_Fails()
        {
            var infoChecker = new AccountService();
            Assert.IsFalse(infoChecker.CheckUsername("  "));
        }

        [TestMethod]
        public void Test_CheckPassword_CorrectInfo_Passes()
        {
            var infoChecker = new AccountService();
            Assert.IsTrue(infoChecker.CheckPassword(password));
        }

        [TestMethod]
        public void Test_CheckPassword_Character_Over_Limit_Fails()
        {
            var infoChecker = new AccountService();
            Assert.IsFalse(infoChecker.CheckPassword("1234567890123456789012345678901"));
        }

        [TestMethod]
        public void Test_CheckPassword_Character_Under_Limit_Fails()
        {
            var infoChecker = new AccountService();
            Assert.IsFalse(infoChecker.CheckPassword("ss"));
        }

        [TestMethod]
        public void Test_CheckPassword_Characters_Not_Permitted()
        {
            var infoChecker = new AccountService();
            Assert.IsFalse(infoChecker.CheckPassword("ÆÆÆ"));
        }
    }
}
