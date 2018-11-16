using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

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

        public Account GetAccountByID(Guid ID)
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

        /// <summary>
        /// The method tries to insert a new row into the database
        /// !!!THIS NEEDS TO BE REFACTORED TO THE NEW ACCOUNT!!!
        /// </summary>
        /// <param name="a">The account to be inserted in the database</param>
        /// <returns>The number of rows affected in the database</returns>
        public int InsertAccount(Account a)
        {
            int affected = 0;
            connection.Open();
            
            // Use transaction in an using block so that it's disposed after the query
            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                // Create sql command
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "insert into Accounts(id, username, email, passwordhash, securitystamp)" +
                    "values(@id, @username, @email, @passwordhash, @securitystamp)";

                // Create and add parameters for the SQL query
                IDbDataParameter param = command.CreateParameter();
                param.ParameterName = "@id";
                param.Value = a.Id;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@username";
                param.Value = a.Username;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@email";
                param.Value = a.Email;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@passwordhash";
                param.Value = a.PasswordHash;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@securitystamp";
                param.Value = a.SecurityStamp;
                command.Parameters.Add(param);

                // Execute query in a try-catch block
                try
                {
                    command.Transaction = transaction;
                    affected = command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                }
            }
            connection.Close();
            return affected;
        }

        public int UpdateAccount(Account oldAccount, Account newAccount)
        {
            throw new NotImplementedException();
        }
    }
}
