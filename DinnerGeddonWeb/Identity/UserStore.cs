using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using DinnergeddonWeb.Models;
using Model;
using System.Collections.Generic;

namespace DinnergeddonWeb.Identity
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserSecurityStampStore<User>,IUserEmailStore<User>, IRoleStore<IdentityRole>, IUserRoleStore<User>
    {
        private readonly AccountServiceReference.AccountServiceClient _proxy;


        public UserStore()
        {
            this._proxy = new AccountServiceReference.AccountServiceClient();
        }

        /// <summary>
        /// Calls the AccountService and Inserts the account into the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task CreateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            Account account = IdentityUserToModelAccount(user);

            return Task.FromResult<bool>(_proxy.InsertAccount(account));
        }

        public Task DeleteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() =>
            {
                //delete account
            });
        }

        public void Dispose()
        {
        }

        public Task<User> FindByIdAsync(string userId)
        {

            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("userId");

            Guid parsedUserId = new Guid(userId);
            if (!Guid.TryParse(userId, out parsedUserId))
                throw new ArgumentOutOfRangeException("userId", string.Format("'{0}' is not a valid GUID.", new { userId }));

            Account account = _proxy.FindById(parsedUserId);
            User user = ModelAccountToIdentityUser(account);


            return Task.FromResult<User>(user);
        }

        public Task<User> FindByIdAsyncOld(User oldUser)
        {

            if (string.IsNullOrWhiteSpace(oldUser.Id))
                throw new ArgumentNullException("userId");

            Guid parsedUserId = new Guid(oldUser.Id);
            if (!Guid.TryParse(oldUser.Id, out parsedUserId))
                throw new ArgumentOutOfRangeException("userId", string.Format("'{0}' is not a valid GUID.", new { oldUser.Id }));

            Account account = _proxy.FindById(parsedUserId);
            User user = ModelAccountToIdentityUser(account);


            return Task.FromResult<User>(user);
        }
        /// <summary>
        /// Converts the Model Account to Identity User. They have the same properties
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Identity Model user object</returns>
        private User ModelAccountToIdentityUser(Account account)
        {
            if (account != null)
            {
                User user = new User()
                {
                    UserName = account.Username,
                    UserId = account.Id,
                    Email = account.Email,
                    PasswordHash = account.PasswordHash,
                    SecurityStamp = account.SecurityStamp
                };
                return user;
            }
            return null;

        }
        /// <summary>
        /// Converts the Identity User to Model Account. They have the same properties
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Model account object</returns>
        private Account IdentityUserToModelAccount(User user)
        {
            if (user != null)
            {
                Account account = new Account()
                {
                    Username = user.UserName,
                    Id = new Guid(user.Id),
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    SecurityStamp = user.SecurityStamp

                };
                return account;
            }
            return null;

        }

        /// <summary>
        /// We use email as the account name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<User> FindByNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");

            Account account = _proxy.FindByEmail(userName);

            User user = ModelAccountToIdentityUser(account);

            return Task.FromResult<User>(user);
        }

        public Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            //return Task.Factory.StartNew(() =>
            //{
            //    
            //_proxy.UpdateAccount();
            return null;
            //});
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public virtual Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        public virtual Task<string> GetSecurityStampAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task SetSecurityStampAsync(User user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

        public Task SetEmailAsync(User user, string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            Account account = _proxy.FindByEmail(email);

            User user = ModelAccountToIdentityUser(account);

            return Task.FromResult<User>(user);
        }

        // RoleStore

        public Task CreateAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }


        //  UserRoleStore

        Task<IdentityRole> IRoleStore<IdentityRole, string>.FindByIdAsync(string roleId)
        {
            throw new NotImplementedException();

        }

        Task<IdentityRole> IRoleStore<IdentityRole, string>.FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            Guid userId = user.UserId;
            _proxy
        
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            Guid userId = user.UserId;
           List<string> roleList = _proxy.GetRoles(userId);
            return Task.FromResult<IList<string>>(roleList);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();



        }
    }
}
//public class CustomUserStore : IUserStore<User>, IUserPasswordStore<User>,
//       IUserSecurityStampStore<User>, IUserLockoutStore<User, string>,
//       IUserTwoFactorStore<User, string>
//{
//    public System.Threading.Tasks.Task<User> FindByNameAsync(string userName)
//    {
//        //Account account;
//        ////User user = InMemoryUserList.DummyUsersList.Find(item => item.Username == userName);
//        //User u = null;
//        //if (account != null)
//        //{
//        //    u = new ApplicationUser()
//        //    {
//        //        Id = user.UserId.ToString(),
//        //        Email = user.Email,
//        //        PasswordHash = user.PasswordHash,
//        //        SecurityStamp = user.SecurityStamp
//        //    };
//        //}

//        return Task.FromResult<User>(u);
//    }

//    public System.Threading.Tasks.Task CreateAsync(User user)
//    {
//        //User u = new User()
//        //{
//        //    UserId = user.UserId,
//        //    Email = user.Email,
//        //    Username = user.UserName,
//        //    PasswordHash = user.PasswordHash,
//        //    SecurityStamp = user.SecurityStamp

//        //};
//        return Task.FromResult<bool>(InMemoryUserList.Add(u));
//    }

//    public Task<string> GetPasswordHashAsync(User user)
//    {
//        return Task.FromResult<string>(user.PasswordHash.ToString());
//    }
//    public Task SetPasswordHashAsync(User user, string passwordHash)
//    {
//        return Task.FromResult<string>(user.PasswordHash = passwordHash);
//    }

//    #region Not implemented methods     
//    public System.Threading.Tasks.Task DeleteAsync(User user)
//    {
//        throw new NotImplementedException();
//    }

//    public System.Threading.Tasks.Task<User> FindByIdAsync(string userId)
//    {
//        //User user = InMemoryUserList.DummyUsersList.Find(item => item.UserId.ToString() == userId);
//        //ApplicationUser u = null;
//        //if (user != null)
//        //{
//        //    u = new ApplicationUser()
//        //    {
//        //        UserId = user.UserId,
//        //        Id = user.UserId.ToString(),
//        //        Email = user.Email,
//        //        PasswordHash = user.PasswordHash,
//        //        SecurityStamp = user.SecurityStamp
//        //    };
//        //}

//        //return Task.FromResult<ApplicationUser>(u);
//    }

//    public System.Threading.Tasks.Task UpdateAsync(User user)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> HasPasswordAsync(User user)
//    {
//        throw new NotImplementedException();
//    }

//    public void Dispose()
//    {
//    }

//    public Task SetSecurityStampAsync(User user, string stamp)
//    {
//        return Task.FromResult<string>(user.SecurityStamp = stamp);
//    }

//    public Task<string> GetSecurityStampAsync(User user)
//    {
//        return Task.FromResult<string>(user.SecurityStamp.ToString());

//    }

//    public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
//    {
//        return Task.FromResult<DateTimeOffset>(DateTimeOffset.UtcNow);
//    }

//    public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
//    {
//        return Task.FromResult<int>(0);

//    }

//    public Task<int> IncrementAccessFailedCountAsync(User user)
//    {
//        return Task.FromResult<int>(0);
//    }

//    public Task ResetAccessFailedCountAsync(User user)
//    {
//        return Task.FromResult<int>(0);

//    }

//    public Task<int> GetAccessFailedCountAsync(User user)
//    {
//        return Task.FromResult<int>(0);
//    }

//    public Task<bool> GetLockoutEnabledAsync(User user)
//    {
//        return Task.FromResult<bool>(false);
//    }

//    public Task SetLockoutEnabledAsync(User user, bool enabled)
//    {
//        return Task.FromResult<int>(0);

//    }

//    public Task SetTwoFactorEnabledAsync(User user, bool enabled)
//    {
//        return Task.FromResult(0);

//    }

//    public Task<bool> GetTwoFactorEnabledAsync(User user)
//    {
//        return Task.FromResult(false);
//    }
//    #endregion
//}



