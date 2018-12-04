using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public interface IAccountController
    {
        /// <summary>
        /// Finds an account given an id
        /// </summary>
        /// <param name="id">The id of an account</param>
        /// <returns>The account found or null</returns>
        Account FindById(Guid id);

        /// <summary>
        /// Finds an account given a username
        /// </summary>
        /// <param name="username">The username of the account</param>
        /// <returns>The account found or null</returns>
        Account FindByUsername(string username);

        /// <summary>
        /// Finds an account given an email
        /// </summary>
        /// <param name="email">The email of the account</param>
        /// <returns>The account found or null</returns>
        Account FindByEmail(string email);

        /// <summary>
        /// Inserts an account
        /// </summary>
        /// <param name="account">The account to insert</param>
        /// <returns>If the operation was successful</returns>
        bool InsertAccount(Account account);

        /// <summary>
        /// Updates an existing account
        /// </summary>
        /// <param name="updatedAccount">An account object with the same ID as the account to be updated</param>
        /// <returns>If the operation was successful</returns>
        bool UpdateAccount(Account updatedAccount);

        /// <summary>
        /// Deletes an existing account
        /// </summary>
        /// <param name="account">The account to delete</param>
        /// <returns>If the operation was successful</returns>
        bool DeleteAccount(Account account);

        /// <summary>
        /// Fetches all accounts currently saved
        /// </summary>
        /// <returns></returns>
        IEnumerable<Account> GetAccounts();

        /// <summary>
        /// Checks if an account has a role
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <param name="roleName">The name of the role</param>
        /// <returns>If the account has a role</returns>
        bool IsInRole(Guid accountId, string roleName);

        /// <summary>
        /// Adds an account to a role
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <param name="roleName">The name of the role</param>
        /// <returns>If the operation was successful</returns>
        bool AddToRole(Guid accountId, string roleName);

        /// <summary>
        /// Gets all the roles an account has
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <returns>A list of all the roles the account has</returns>
        IEnumerable<string> GetAccountRoles(Guid accountId);

        /// <summary>
        /// Verifies that the credentials of the account are valid
        /// </summary>
        /// <param name="name">The username or email of the account</param>
        /// <param name="password">The password of the account</param>
        /// <returns>If the credentials are valid</returns>
        Account VerifyCredentials(string name, string password);

        bool GetEmailConfirmed(Account account);

        void SetEmailConfirmed(Account account, bool confirmed);
    }
}
