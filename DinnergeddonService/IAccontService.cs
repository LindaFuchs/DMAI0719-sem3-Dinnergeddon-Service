using Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace DinnergeddonService
{
    [ServiceContract]
    public interface IAccontService
    {
        [OperationContract]
        Account FindById(Guid id);
        [OperationContract]
        Account FindByEmail(string email);
        [OperationContract]
        Account FindByUsername(string username);

        [OperationContract]
        bool InsertAccount(Account account);
        [OperationContract]
        bool UpdateAccount(Account updatedAccount);
        [OperationContract]
        bool DeleteAccount(Account account);

        [OperationContract]
        IEnumerable<Account> GetAccounts();

        [OperationContract]
        bool IsInRole(Guid accountId, string roleName);
        [OperationContract]
        bool AddToRole(Guid accountId, string roleName);
        [OperationContract]
        IEnumerable<string> GetRoles(Guid accountId);
    }
}
