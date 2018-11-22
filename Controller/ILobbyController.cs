using Model;
using System.Collections.Generic;

namespace Controller
{
    interface ILobbyController
    {
        Lobby CreateLobby(string name, int playerLimit);
        Lobby CreateLobby(string name, int playerLimit, string password);
        IEnumerable<Lobby> GetLobbies();
    }
}
