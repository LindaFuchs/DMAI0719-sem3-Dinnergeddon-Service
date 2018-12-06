using Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DBLayer
{
    public class AccountRepo : IAccountRepo
    {
        private readonly IDbConnection connection;

        public AccountRepo(IDbComponents components)
        {
            connection = components.Connection;
        }

        /// <summary>
        /// This method adds a role to an already existing account with accountID
        /// </summary>
        /// <param name="accountID">The ID of the account</param>
        /// <param name="roleName">The name of the role</param>
        /// <returns>Number of rows affected</returns>
        public bool AddToRole(Guid accountID, string roleName)
        {
            int affected = 0;
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "insert into accountroles(accountid, roleid) " +
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

            if (affected < 0 || affected > 1)
                throw new InvalidOperationException("Something went wrong with the DB");

            return affected == 1;
        }

        /// <summary>
        /// This method tries to delete a row in the database given an account information
        /// </summary>
        /// <param name="a">The account that's to be deleted from the database</param>
        /// <returns>The number of rows affected</returns>
        public bool DeleteAccount(Account a)
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

            if (affected < 0 || affected > 1)
                throw new InvalidOperationException("Something went wrong in the database");

            return affected == 1;
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

                try
                {
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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
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

                try
                {
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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
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
                try
                {
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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
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
                try
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accounts.Add(Build(reader));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            connection.Close();
            return accounts;
        }


        /// <summary>
        /// This method gets all the roles an account currently has
        /// </summary>
        /// <param name="accountID">The account ID to check</param>
        /// <returns>A list of all the roles an account has</returns>
        public IEnumerable<string> GetRoles(Guid accountID)
        {
            List<string> roles = new List<string>();
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select r.Name " +
                    "from roles as r " +
                    "join accountRoles as ar on r.id = ar.roleid " +
                    "join accounts as a on a.id = ar.accountid " +
                    "where a.id=@id";

                // Escape SQL injection with parameters
                IDataParameter param = command.CreateParameter();
                param.ParameterName = "@id";
                param.Value = accountID;
                command.Parameters.Add(param);

                try
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(reader.GetString(0));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            connection.Close();
            return roles;
        }

        /// <summary>
        /// The method tries to insert a new row into the database
        /// </summary>
        /// <param name="a">The account to be inserted in the database</param>
        /// <returns>The number of rows affected in the database</returns>
        public bool InsertAccount(Account a)
        {
            int affected = 0;
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                // Create SQL command
                command.CommandText = "insert into Accounts(id, username, email, passwordhash, securitystamp) " +
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

            if (affected < 0 || affected > 1)
                throw new InvalidOperationException("Something went wrong with the query");

            return affected == 1;
        }

        /// <summary>
        /// This method checks if an account has a role
        /// </summary>
        /// <param name="accountID">The account ID to check</param>
        /// <param name="roleName">The name of the role to check</param>
        /// <returns>If an account with accountID has a role with roleName</returns>
        public bool IsInsideRole(Guid accountID, string roleName)
        {
            // Assume that the user doesn't have it until proven wrong
            bool hasRole = false;

            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select * from Roles as r " +
                    "join AccountRoles as ar on r.id = ar.roleid " +
                    "join Accounts as a on ar.accountid = a.ID " +
                    "where a.id=@id and r.name=@rolename";

                // Create and add parameters for the SQL query
                IDbDataParameter param = command.CreateParameter();
                param.ParameterName = "@id";
                param.Value = accountID;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@rolename";
                param.Value = roleName;
                command.Parameters.Add(param);

                try
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hasRole = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            connection.Close();
            return hasRole;
        }


        /// <summary>
        /// This method tries to update an account in the table.
        /// </summary>
        /// <param name="account">The new information of accont, ID is kept the same</param>
        /// <returns>Number of rows affected</returns>
        public bool UpdateAccount(Account account)
        {
            int affected = 0;
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "update Accounts set " +
                        "username=@username, " +
                        "email=@email, " +
                        "passwordhash=@passwordhash, " +
                        "securitystamp=@securitystamp " +
                    "where " +
                        "id=@id";

                // Escape SQL injections
                IDataParameter param = command.CreateParameter();
                param.ParameterName = "@id";
                param.Value = account.Id;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@username";
                param.Value = account.Username;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@email";
                param.Value = account.Email;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@passwordhash";
                param.Value = account.PasswordHash;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@securitystamp";
                param.Value = account.SecurityStamp;
                command.Parameters.Add(param);

                using (IDbTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable))
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

            if (affected < 0 || affected > 1)
                throw new InvalidOperationException("Something went wrong with the DB");

            return affected == 1;
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

        /// <summary>
        /// Sets EmailConfirmed to the value of confirmed
        /// </summary>
        /// <param name="account"></param>
        /// <param name="confirmed"></param>
        public void SetEmailConfirmed(Account account, bool confirmed)
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "update Accounts set " +
                        "EmailConfirmed=@confirmed " +
                    "where " +
                        "id=@id";

                // Escape SQL injections
                IDataParameter param = command.CreateParameter();
                param.ParameterName = "@id";
                param.Value = account.Id;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@confirmed";
                param.Value = confirmed;
                command.Parameters.Add(param);



                using (IDbTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    command.Transaction = transaction;
                    try
                    {
                        command.ExecuteNonQuery();
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

        }

        /// <summary>
        /// Checks the EmailConfirmed field for the account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool GetEmailConfirmed(Account account)
        {
            // Assume that the user doesn't have it until proven wrong
            bool emailConfirmed = false;

            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select EmailConfirmed from Accounts " +
                    "where id=@id";

                IDataParameter param = command.CreateParameter();
                param.ParameterName = "@id";
                param.Value = account.Id;
                command.Parameters.Add(param);

                try
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            emailConfirmed = reader.GetBoolean(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            connection.Close();
            return emailConfirmed;
        }

        public bool RemoveFromRole(Guid accountId, string roleName)
        {
            connection.Open();
            int affected = 0;
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "delete from AccountRoles " +
                    "where AccountId=@id and " +
                    "RoleId=(select id from roles where name=@rolename)";

                // Escape SQL injection possibilities
                IDataParameter param = command.CreateParameter();
                param.ParameterName = "@id";
                param.Value = accountId;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.ParameterName = "@rolename";
                param.Value = roleName;
                command.Parameters.Add(param);

                using (IDbTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable))
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

            if (affected < 0 || affected > 1)
                throw new InvalidOperationException("Something went wrong with the DB");

            return affected == 1;
        }
    }
}
