using Model;
using System.Collections.Generic;
using System;

namespace DBLayer
{
    public interface IAccountRepo
    {
        /// <summary>
        /// Gets all accounts that are saved on the system
        /// </summary>
        /// <returns>A list of all accounts saved on the system</returns>
        IEnumerable<Account> GetAccounts();

        /// <summary>
        /// Gets an account given an ID
        /// </summary>
        /// <param name="accountID">The ID of the account</param>
        /// <returns>An account or null</returns>
        Account GetAccountByID(Guid accountID);

        /// <summary>
        /// Gets an account given a username
        /// </summary>
        /// <param name="username">The username of the account</param>
        /// <returns>An account or null</returns>
        Account GetAccountByUsername(string username);

        /// <summary>
        /// Gets an account given an email
        /// </summary>
        /// <param name="email">The email of the account</param>
        /// <returns>An account or null</returns>
        Account GetAccountByEmail(string email);

        /// <summary>
        /// Inserts a new account
        /// </summary>
        /// <param name="a">The new account to be inserted</param>
        /// <returns>The number of rows affected</returns>
        bool InsertAccount(Account a);

        /// <summary>
        /// Deletes an existing account
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        bool DeleteAccount(Account a);

        /// <summary>
        /// Updates an existing account
        /// </summary>
        /// <param name="newAccount">An account object with the ID of the account to be updated</param>
        /// <returns>If the operation was successful or not</returns>
        bool UpdateAccount(Account newAccount);

        /// <summary>
        /// Adds an account to a role
        /// </summary>
        /// <param name="accountID">The ID of the account</param>
        /// <param name="roleName">The name of the role</param>
        /// <returns>If the operation was successful</returns>
        bool AddToRole(Guid accountID, string roleName);

        /// <summary>
        /// Checks if an account is in a role
        /// </summary>
        /// <param name="accountID">The ID of the account</param>
        /// <param name="roleName">The name of the role</param>
        /// <returns>If the account has a role</returns>
        bool IsInsideRole(Guid accountID, string roleName);

        /// <summary>
        /// Gets all the roles an account has
        /// </summary>
        /// <param name="accountID">The ID of the account</param>
        /// <returns>A list of all the roles an account has</returns>
        IEnumerable<string> GetRoles(Guid accountID);
    }
}
