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

                    UserId = Guid.NewGuid(),
                    Username = user.UserName,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    SecurityStamp = user.SecurityStamp
                };

                //the service interface Register method should accept Account model object instead of parameters
                _proxy.Register(account);

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

            //return Task.Factory.StartNew(() =>
            //{
            //   //return user 
            //});
        }

        public Task<User> FindByNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");

            //return Task.Factory.StartNew(() =>
            //{
            //    return user
            //});
        }

        public Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            //return Task.Factory.StartNew(() =>
            //{
            //    //update user
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
}