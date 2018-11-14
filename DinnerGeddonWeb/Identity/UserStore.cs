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

            return Task.Factory.StartNew(() => {

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
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}