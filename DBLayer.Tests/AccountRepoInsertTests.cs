using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Data;
using System.Data.Common;

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

    class DbComponentsMock : IDbComponents
    {
        public DbComponentsMock(IDbConnection connection, DbProviderFactory factory)
        {
            Connection = connection;
            Factory = factory;
        }

        public IDbConnection Connection { get; }
        public DbProviderFactory Factory { get; }
    }

    class DbConnectionMock : IDbConnection
    {
        public bool connectionOpened;
        public bool connectionClosed;

        public DbTransactionMock transaction;

        public bool transactionUsed;

        public DbConnectionMock()
        {
            connectionOpened = false;
            connectionClosed = false;
            transactionUsed = false;
        }

        public string ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int ConnectionTimeout => throw new NotImplementedException();

        public string Database => throw new NotImplementedException();

        public ConnectionState State => throw new NotImplementedException();

        public IDbTransaction BeginTransaction()
        {
            transaction = new DbTransactionMock();
            transactionUsed = true;
            return transaction;
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new NotImplementedException();
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            connectionClosed = true;
        }

        public IDbCommand CreateCommand()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            connectionOpened = true;
        }
    }

    class DbProviderFactoryMock : DbProviderFactory
    {

    }

    class DbTransactionMock : IDbTransaction
    {
        public bool commited;
        public bool rollbacked;

        public DbTransactionMock()
        {
            commited = false;
            rollbacked = false;
        }

        public IDbConnection Connection => throw new NotImplementedException();

        public IsolationLevel IsolationLevel => throw new NotImplementedException();

        public void Commit()
        {
            commited = true;
        }

        public void Dispose()
        {
            return;
        }

        public void Rollback()
        {
            rollbacked = true;
        }
    }
}
