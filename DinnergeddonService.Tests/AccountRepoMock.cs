using DBLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinnergeddonService.Tests
{
    class AccountRepoMock : IAccountRepo
    {
        public int AddToRole(Guid accountID, string roleName)
        {
            throw new NotImplementedException();
        }

        public int DeleteAccount(Account a)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountByID(Guid accountID)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAccounts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetRoles(Guid accountID)
        {
            throw new NotImplementedException();
        }

        public int InsertAccount(Account a)
        {
            throw new NotImplementedException();
        }

        public bool IsInsideRole(Guid accountID, string roleName)
        {
            throw new NotImplementedException();
        }

        public int UpdateAccount(Account newAccount)
        {
            throw new NotImplementedException();
        }
    }
}
