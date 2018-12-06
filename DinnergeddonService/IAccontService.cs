using Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace DinnergeddonService
{
    [ServiceContract]
    public interface IAccountService
    {
        /// <summary>
        /// Finds an account given an ID
        /// </summary>
        /// <param name="id">The id of the account</param>
        /// <returns>The account found or null</returns>
        [OperationContract]
        Account FindById(Guid id);

        /// <summary>
        /// Finds an account given an email
        /// </summary>
        /// <param name="email">The email of the account</param>
        /// <returns>The account found or null</returns>
        [OperationContract]
        Account FindByEmail(string email);

        /// <summary>
        /// Finds an account given a username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [OperationContract]
        Account FindByUsername(string username);

        /// <summary>
        /// Inserts an account
        /// </summary>
        /// <param name="account">The account to be inserted</param>
        /// <returns>If the operation was successful</returns>
        [OperationContract]
        bool InsertAccount(Account account);

        /// <summary>
        /// Updates an account
        /// </summary>
        /// <param name="updatedAccount">An account object with the ID of the account to be updated</param>
        /// <returns>If the operation was successful</returns>
        [OperationContract]
        bool UpdateAccount(Account updatedAccount);

        /// <summary>
        /// Deletes an existing account from the system
        /// </summary>
        /// <param name="account">The account to be deleted</param>
        /// <returns>If the operation was successful</returns>
        [OperationContract]
        bool DeleteAccount(Account account);

        //TODO: Make this only callable by the admins
        /// <summary>
        /// Gets all accounts saved on the system
        /// </summary>
        /// <returns>A list of all accounts saved on the system</returns>
        [OperationContract]
        IEnumerable<Account> GetAccounts();

        /// <summary>
        /// Checks if an account has a role
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <param name="roleName">The name of the role</param>
        /// <returns>If the account has the role</returns>
        [OperationContract]
        bool IsInRole(Guid accountId, string roleName);

        /// <summary>
        /// Adds a role to an account
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <param name="roleName">The name of the role</param>
        /// <returns>If the operation was successful</returns>
        [OperationContract]
        bool AddToRole(Guid accountId, string roleName);

        /// <summary>
        /// Gets all roles that an account has
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <returns>A list of all the roles the account has</returns>
        [OperationContract]
        IEnumerable<string> GetRoles(Guid accountId);

        /// <summary>
        /// Verifies that the credentials of an account are correct
        /// </summary>
        /// <param name="name">The username or email of the account</param>
        /// <param name="password">The password of the account</param>
        /// <returns>The account that was authenticated or null if it wasn't</returns>
        [OperationContract]
        Account VerifyCredentials(string name, string password);

        [OperationContract]
        bool GetEmailConfirmed(Account account);

        [OperationContract]
        void SetEmailConfirmed(Account account, bool confirmed);

        [OperationContract]
        bool RemoveFromRole(Guid accountId, string roleName);
    }
}
