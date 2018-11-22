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

        public bool AddToRole(Guid accountId, string roleName)
        {
            int affectedRows = accountRepo.AddToRole(accountId, roleName);

            if (affectedRows > 1)
                throw new Exception("More than one rows were changed in the database");
            
            return affectedRows == 1;
        }

        public Account FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Account FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Account FindByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAccountRoles(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAccounts()
        {
            throw new NotImplementedException();
        }

        public bool InsertAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public bool IsInRole(Guid accountId, string roleName)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAccount(Account updatedAccount)
        {
            throw new NotImplementedException();
        }
    }
}
