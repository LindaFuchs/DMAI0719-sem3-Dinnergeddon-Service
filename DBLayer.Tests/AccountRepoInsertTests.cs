using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using static DBLayer.Tests.AccountRepoMocks;

namespace DBLayer.Tests
{
    [TestClass]
    public class AccountRepoInsertTests
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
        public void Test_InsertAccount_OpensAndClosesConneciton_Passes()
        {
            // Setup
            IAccountRepo accountRepo = new AccountRepo(componentsMock);
            Account a = new Account();

            // Action
            accountRepo.InsertAccount(a);

            // Assert
            Assert.IsTrue(connectionMock.connectionOpened);
            Assert.IsTrue(connectionMock.connectionClosed);
        }

        [TestMethod]
        public void Test_InsertAccount_UsesTransactionAndCommits_Passes()
        {
            // Setup
            IAccountRepo accountRepo = new AccountRepo(componentsMock);
            Account a = new Account();

            // Action
            accountRepo.InsertAccount(a);

            // Assert
            Assert.IsTrue(connectionMock.transactionUsed);
            Assert.IsTrue(connectionMock.transaction.commited);
        }

        [TestMethod]
        public void Test_InsertAccount_TransactionRolledBackAfterSqlFail_Passes()
        {
            // Setup
            IAccountRepo accountRepo = new AccountRepo(componentsMock);
            Account a = new Account() { Id = new Guid("123") };
            Account b = new Account() { Id = new Guid("123") };
            
            // Action
            accountRepo.InsertAccount(a);

            accountRepo.InsertAccount(b);

            // Assert
            Assert.IsTrue(connectionMock.transactionUsed);
            Assert.IsTrue(connectionMock.transaction.rollbacked);
        }

        [TestMethod]
        public void Test_InsertAccount_Passes()
        {
            throw new NotImplementedException();
        }

        public void InsertAccountReturnsCorrectNumberOfRowsAffected()
        {
            throw new NotImplementedException();
        }
    }
}
