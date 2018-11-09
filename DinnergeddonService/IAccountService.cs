using System.ServiceModel;

namespace DinnergeddonService
{
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        bool Registration(string name, string email, string password, int id);
    }
}
