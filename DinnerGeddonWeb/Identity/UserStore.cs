using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using DinnergeddonWeb.Models;
using Model;

namespace DinnergeddonWeb.Identity
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserSecurityStampStore<User>
    {
        private readonly AccountServiceReference.AccountServiceClient _proxy;

        public UserStore()
        {
            this._proxy = new AccountServiceReference.AccountServiceClient();
        }

        public Task CreateAsync(User user)
        {

            if (user == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() =>
            {
                
                //converting Identity model to the Account class in the project Model
                Account account = new Account()
                {

                    Id = Guid.NewGuid(),
                    Username = user.UserName,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    SecurityStamp = user.SecurityStamp
                };

                //the service interface Register method should accept Account model object instead of parameters
                //  _proxy.Register(account);
                return IdentityResult.Success;

            });
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

            Guid parsedUserId;
            if (!Guid.TryParse(userId, out parsedUserId))
                throw new ArgumentOutOfRangeException("userId", string.Format("'{0}' is not a valid GUID.", new { userId }));

            return Task.Factory.StartNew(() =>
            {
                //
            });
        }

        public Task<User> FindByNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");

            return Task.Factory.StartNew(() =>
            {
                //    return user
                // get and return a user if it exists, if not create an empty one
                return new User();
            });
        }

        public Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            //return Task.Factory.StartNew(() =>
            //{
            //    //update user
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
}

