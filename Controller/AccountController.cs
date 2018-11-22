using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DBLayer;

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
            int affectedRows = accountRepo.AddToRole(accountId, roleName);

            if (affectedRows > 1)
                throw new Exception("More than one rows were changed in the database");
            
            return affectedRows == 1;
        }

        /// <summary>
        /// Finds an account searching by email
        /// </summary>
        /// <param name="email">The email to search by</param>
        /// <returns>An account with email</returns>
        public Account FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds an account searching by ID
        /// </summary>
        /// <param name="id">The ID to search by</param>
        /// <returns>An account with id</returns>
        public Account FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds an account searching by username
        /// </summary>
        /// <param name="username">The username to search by</param>
        /// <returns>An account with username</returns>
        public Account FindByUsername(string username)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all roles an account with accountId has
        /// </summary>
        /// <param name="accountId">The id of an account</param>
        /// <returns>A list of all the roles an account has</returns>
        public IEnumerable<string> GetAccountRoles(Guid accountId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all accounts saved on the system
        /// </summary>
        /// <returns>A list of all accounts saved on the system</returns>
        public IEnumerable<Account> GetAccounts()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a new account to the system
        /// </summary>
        /// <param name="account">A new account</param>
        /// <returns>If the operation was successful</returns>
        public bool InsertAccount(Account account)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if an account with accoundId has a role
        /// </summary>
        /// <param name="accountId">The ID of an account</param>
        /// <param name="roleName">The name of the role</param>
        /// <returns>If the account has a role</returns>
        public bool IsInRole(Guid accountId, string roleName)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
