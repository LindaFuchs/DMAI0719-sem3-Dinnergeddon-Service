using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DBLayer.Tests.AccountRepoMocks;

namespace DBLayer.Tests
{
    [TestClass]
    class AccountRepoUpdateTests
    {
        private DbConnectionMock connectionMock;
        private DbProviderFactoryMock factoryMock;
        private DbComponentsMock componentsMock;

        [TestInitialize]
        public void Initialize()
        {
            connectionMock = new DbConnectionMock();
            factoryMock = new DbProviderFactoryMock();
            componentsMock = new DbComponentsMock(connectionMock, factoryMock);
        }

        [TestMethod]
        public void Test_UpdateAccount_UsesTransaction_Passes()
        {
            // Setup
            IAccountRepo accountRepo = new AccountRepo(componentsMock);
            Account a = new Account();
            Account b = new Account();

            // Action
            accountRepo.UpdateAccount(a, b);
            
            // Assert
            Assert.IsTrue(connectionMock.transactionUsed);
        }

        [TestMethod]
        public void Test_UpdateAccount_UsesCommit_Passes()
        {
            // Setup
            IAccountRepo accountRepo = new AccountRepo(componentsMock);
            Account a = new Account();
            Account b = new Account();

            // Action
            accountRepo.UpdateAccount(a, b);

            // Assert
            Assert.IsTrue(connectionMock.transaction.commited);
        }
    }

}
