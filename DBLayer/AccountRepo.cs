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

        public int AddToRole(Guid accountID, string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method adds a role to an already existing account with accountID
        /// </summary>
        /// <param name="accountID">The ID of the account</param>
        /// <param name="roleName">The name of the role</param>
        /// <returns>Number of rows affected</returns>
        public int AddToRole(Guid accountID, string roleName)
        {
            int affected = 0;
            connection.Open();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "insert into accountroles(accountid, roleid)" +
                    "values(@accountid, (select id from roles where name=@rolename))";

                // Create parameters, escape SQL injection
                IDataParameter param = command.CreateParameter();
                param.ParameterName = "@accountid";
                param.Value = accountID;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@rolename";
                param.Value = roleName;
                command.Parameters.Add(param);

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    command.Transaction = transaction;

                    try
                    {
                        affected = command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Console.WriteLine(e.Message);
                    }
                }
            }

            connection.Close();
            return affected;
        }

        /// <summary>
        /// This method tries to delete a row in the database given an account information
        /// </summary>
        /// <param name="a">The account that's to be deleted from the database</param>
        /// <returns>The number of rows affected</returns>
        public int DeleteAccount(Account a)
        {
            int affected = 0;
            connection.Open();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "delete from accounts where " +
                    "id=@id and " +
                    "username=@username and " +
                    "email=@email and " +
                    "passwordhash=@passwordhash and " +
                    "securitystamp=@securitystamp";

                // Create parameters, escape SQL injections
                IDataParameter param = command.CreateParameter();
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

                // Execute query with transaction
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    command.Transaction = transaction;
                    try
                    {
                        affected = command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Console.WriteLine(e.Message);
                    }
                }
            }
            connection.Close();
            return affected;
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

        /// <summary>
        /// This method tries to select an account from the database given a username
        /// </summary>
        /// <param name="username">The username by which the account is to be found</param>
        /// <returns>An account with username</returns>
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

        /// <summary>
        /// This method selects all rows from the Accounts table
        /// </summary>
        /// <returns>A list of all rows in Accounts table</returns>
        public IEnumerable<Account> GetAccounts()
        {
            List<Account> accounts = new List<Account>();

            connection.Open();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select * from Accounts";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        accounts.Add(Build(reader));
                    }
                }
            }
            connection.Close();

            return accounts;
        }

        public IEnumerable<string> GetRoles(Guid accountID)
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

            using (IDbCommand command = connection.CreateCommand())
            {
                // Create SQL command
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

                // Use transaction in an using block so that it's disposed after the query
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    command.Transaction = transaction;
                    try
                    {
                        affected = command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        transaction.Rollback();
                    }
                }
            }
            connection.Close();
            return affected;
        }

        public bool IsInsideRole(Guid accountID, string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method tries to update an account in the table. It uses the optimistic concurrency approach
        /// </summary>
        /// <param name="oldAccount">The information of the old account</param>
        /// <param name="newAccount">The new information of the same account</param>
        /// <returns>Number of rows affected</returns>
        public int UpdateAccount(Account oldAccount, Account newAccount)
        {
            int affected = 0;
            connection.Open();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "update Accounts set" +
                        "id=@nid," +
                        "username=@nusername," +
                        "email=@nemail," +
                        "passwordhash=@npasswordhash," +
                        "securitystamp=@nsecuritystamp," +
                    "where " +
                        "id=@oid," +
                        "username=@ousername," +
                        "email=@oemail," +
                        "passwordhash=@opasswordhash," +
                        "securitystamp=@osecuritystamp";

                IDataParameter param = command.CreateParameter();
                param.ParameterName = "@nid";
                param.Value = newAccount.Id;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@nusername";
                param.Value = newAccount.Username;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@nemail";
                param.Value = newAccount.Email;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@npasswordhash";
                param.Value = newAccount.PasswordHash;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@nsecuritystamp";
                param.Value = newAccount.SecurityStamp;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@oid";
                param.Value = oldAccount.Id;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@ousername";
                param.Value = oldAccount.Username;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@oemail";
                param.Value = oldAccount.Email;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@opasswordhash";
                param.Value = oldAccount.PasswordHash;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@osecuritystamp";
                param.Value = oldAccount.SecurityStamp;
                command.Parameters.Add(param);

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    command.Transaction = transaction;

                    try
                    {
                        affected = command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        transaction.Rollback();
                    }
                }
            }

            connection.Close();
            return affected;
        }

        /// <summary>
        /// This method builds an Account object
        /// </summary>
        /// <param name="reader">a IDataReader instance with an Account record</param>
        /// <returns>An account object built from a reader record</returns>
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
