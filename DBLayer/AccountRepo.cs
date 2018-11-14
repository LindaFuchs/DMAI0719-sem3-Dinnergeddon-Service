using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DBLayer
{
    public class AccountRepo : IAccountRepo
    {
        private readonly DbProviderFactory factory;
        private readonly IDbConnection connection;

        public AccountRepo(IDbComponents components)
        {
            factory = components.Factory;
            connection = components.Connection;
        }

        public int DeleteAccount(Account a)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountByID(int ID)
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

        public int InsertAccount(Account a)
        {
            connection.Open();
            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                transaction.Commit();
            }
            connection.Close();
            return 0;
        }

        public int UpdateAccount(Account oldAccount, Account newAccount)
        {
            throw new NotImplementedException();
        }
    }
}
