using System.ServiceModel;
using Model;

namespace DinnergeddonService
{
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        bool Register(string username, string email, string password, int id);
        [OperationContract]
        bool CheckUsername(string username);
        [OperationContract]
        bool CheckEmail(string email);
        [OperationContract]
        Account GetInfo();
        [OperationContract]
        bool EditAccount(string username, string email, string password);
        [OperationContract]
        bool Login(string username, string password);
    }
}
