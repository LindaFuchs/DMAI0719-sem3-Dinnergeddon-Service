using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public interface IAccountController
    {
        Account FindById(Guid id);
        Account FindByUsername(string username);
        Account FindByEmail(string email);

        bool InsertAccount(Account account);
        bool UpdateAccount(Account updatedAccount);
        bool DeleteAccount(Account account);

        IEnumerable<Account> GetAccounts();

        bool IsInRole(Guid accountId, string roleName);
        bool AddToRole(Guid accountId, string roleName);

        IEnumerable<string> GetAccountRoles(Guid accountId);
    }
}
