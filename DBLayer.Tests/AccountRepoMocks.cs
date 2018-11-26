using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.Tests
{
    class AccountRepoMocks
    {


        public class DbComponentsMock : IDbComponents
        {
            public DbComponentsMock(IDbConnection connection, DbProviderFactory factory)
            {
                Connection = connection;
                Factory = factory;
            }

            public IDbConnection Connection { get; }
            public DbProviderFactory Factory { get; }

            IDbConnection IDbComponents.Connection => throw new NotImplementedException();
        }

        public class DbConnectionMock : IDbConnection
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

        public class DbProviderFactoryMock : DbProviderFactory
        {

        }

        public class DbTransactionMock : IDbTransaction
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
}
