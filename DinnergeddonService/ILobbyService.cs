using Model;
using System.ServiceModel;
using System.Collections.Generic;

namespace DinnergeddonService
{
    [ServiceContract]
    interface ILobbyService
    {
        [OperationContract]
        Lobby CreateLobby(string name, int playerLimit);
        [OperationContract]
        Lobby CreateLobby(string name, int playerLimit, string password);
        [OperationContract]
        IEnumerable<Lobby> GetLobbies();
    }
}
