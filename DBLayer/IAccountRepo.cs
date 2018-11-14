using Model;
using System.Collections.Generic;

namespace DBLayer
{
    public interface IAccountRepo
    {
        IEnumerable<Account> GetAccounts();

        Account GetAccountByID(int ID);
        Account GetAccountByUsername(string username);
        Account GetAccountByEmail(string email);

        int InsertAccount(Account a);
        int DeleteAccount(Account a);
        int UpdateAccount(Account oldAccount, Account newAccount);
    }
}
