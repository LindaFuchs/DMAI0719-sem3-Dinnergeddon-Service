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

        /// <summary>
        /// This method tries to find an account in the database given an email
        /// </summary>
        /// <param name="email">The email of the account that's to be found in the database</param>
        /// <returns>An account with email</returns>
        public Account GetAccountByEmail(string email)
        {
            Account account = null;
            connection.Open();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select * from Accounts where email=@email";

                // Escape SQL injections
                IDbDataParameter param = command.CreateParameter();
                param.ParameterName = "@email";
                param.Value = email;
                command.Parameters.Add(param);

                using (IDataReader reader = command.ExecuteReader())
                {
                    // Check if we actually have a row from the DB, if not throw an exception
                    if (!reader.Read())
                    {
                        connection.Close();
                        throw new KeyNotFoundException("An account with this email was not found");
                    }

                    account = Build(reader);
                }
            }

            connection.Close();
            return account;
        }

        /// <summary>
        /// This method tries to find an account in the database given an ID
        /// </summary>
        /// <param name="ID">The ID of the account that's to be found in the database</param>
        /// <returns>An account with ID</returns>
        public Account GetAccountByID(Guid ID)
        {
            Account account = null;
            connection.Open();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select * from Accounts where id=@id";

                // Escape SQL injections
                IDbDataParameter param = command.CreateParameter();
                param.ParameterName = "@id";
                param.Value = ID;
                command.Parameters.Add(param);

                using (IDataReader reader = command.ExecuteReader())
                {
                    // Check if we actually have a row from the DB, if not throw an exception
                    if (!reader.Read())
                    {
                        connection.Close();
                        throw new KeyNotFoundException("An account with this ID was not found");
                    }

                    account = Build(reader);
                }
            }

            connection.Close();
            return account;
        }

        public Account GetAccountByUsername(string username)
        {
            Account account = null;
            connection.Open();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select * from Accounts where username=@username";

                // Escape SQL injections
                IDbDataParameter param = command.CreateParameter();
                param.ParameterName = "@username";
                param.Value = username;
                command.Parameters.Add(param);

                using (IDataReader reader = command.ExecuteReader())
                {
                    // Check if we actually have a row from the DB, if not throw an exception
                    if (!reader.Read())
                    {
                        connection.Close();
                        throw new KeyNotFoundException("An account with this username was not found");
                    }

                    account = Build(reader);
                }
            }

            connection.Close();
            return account;
        }

        public IEnumerable<Account> GetAccounts()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The method tries to insert a new row into the database
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

        private Account Build(IDataReader reader)
        {
            Account a = new Account()
            {
                Id = reader.GetGuid(0),
                Username = reader.GetString(1),
                Email = reader.GetString(2),
                PasswordHash = reader.GetString(3),
                SecurityStamp = reader.GetString(4),
            };

            return a;
        }
    }
}
