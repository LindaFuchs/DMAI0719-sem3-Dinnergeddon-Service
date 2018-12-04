using DBLayer;
using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class AccountController : IAccountController
    {
        private readonly IAccountRepo accountRepo = new AccountRepo(DbComponents.GetInstance());

        /// <summary>
        /// Adds an account with accountId to a role
        /// </summary>
        /// <param name="accountId">The ID of the account to be changed</param>
        /// <param name="roleName">The role the account is to be added to</param>
        /// <returns>If the operation was successful</returns>
        public bool AddToRole(Guid accountId, string roleName)
        {
            return accountRepo.AddToRole(accountId, roleName);
        }

        /// <summary>
        /// Finds an account searching by email
        /// </summary>
        /// <param name="email">The email to search by</param>
        /// <returns>An account with email</returns>
        public Account FindByEmail(string email)
        {
            return accountRepo.GetAccountByEmail(email);
        }

        /// <summary>
        /// Finds an account searching by ID
        /// </summary>
        /// <param name="id">The ID to search by</param>
        /// <returns>An account with id</returns>
        public Account FindById(Guid id)
        {
            return accountRepo.GetAccountByID(id);
        }

        /// <summary>
        /// Finds an account searching by username
        /// </summary>
        /// <param name="username">The username to search by</param>
        /// <returns>An account with username</returns>
        public Account FindByUsername(string username)
        {
            return accountRepo.GetAccountByUsername(username);
        }

        /// <summary>
        /// Gets all roles an account with accountId has
        /// </summary>
        /// <param name="accountId">The id of an account</param>
        /// <returns>A list of all the roles an account has</returns>
        public IEnumerable<string> GetAccountRoles(Guid accountId)
        {
            return accountRepo.GetRoles(accountId);
        }

        /// <summary>
        /// Gets all accounts saved on the system
        /// </summary>
        /// <returns>A list of all accounts saved on the system</returns>
        public IEnumerable<Account> GetAccounts()
        {
            return accountRepo.GetAccounts();
        }

        /// <summary>
        /// Adds a new account to the system
        /// </summary>
        /// <param name="account">A new account</param>
        /// <returns>If the operation was successful</returns>
        public bool InsertAccount(Account account)
        {
            return accountRepo.InsertAccount(account);
        }

        /// <summary>
        /// Checks if an account with accoundId has a role
        /// </summary>
        /// <param name="accountId">The ID of an account</param>
        /// <param name="roleName">The name of the role</param>
        /// <returns>If the account has a role</returns>
        public bool IsInRole(Guid accountId, string roleName)
        {
            return accountRepo.IsInsideRole(accountId, roleName);
        }

        /// <summary>
        /// Updates an existing account.
        /// Since the ID of an account is immutable (cannot change), the account that's going to be changed will be the one
        /// that has the ID of the account passed to this method
        /// </summary>
        /// <param name="updatedAccount">The new information of the account</param>
        /// <returns>If the operation was successful</returns>
        public bool UpdateAccount(Account updatedAccount)
        {
            return accountRepo.UpdateAccount(updatedAccount);
        }

        /// <summary>
        /// Deletes an account from the system.
        /// </summary>
        /// <param name="account">The account to be deleted</param>
        /// <returns>If the account was deleted successfuly</returns>
        public bool DeleteAccount(Account account)
        {
            return accountRepo.DeleteAccount(account);
        }

        /// <summary>
        /// Verifies that the credentials of the account are valid
        /// </summary>
        /// <param name="name">The username or email of the account</param>
        /// <param name="password">The password of the account</param>
        /// <returns>If the credentials are valid</returns>
        public Account VerifyCredentials(string name, string password)
        {
            Account account;
            if (name.Contains(@"@"))
                account = FindByEmail(name);
            else
                account = FindByUsername(name);

            // Account doesn't exist, return null
            if (account == null)
                return null;

            // Return account if password is correct, else return null
            if (PasswordHasher.VerifyPassword(account.PasswordHash, password))
                return account;
            else
                return null;
        }
    }
}
