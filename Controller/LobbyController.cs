using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    class LobbyController : ILobbyController
    {
        public Lobby CreateLobby(string name, int playerLimit)
        {
            throw new NotImplementedException();
        }

        public Lobby CreateLobby(string name, int playerLimit, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lobby> GetLobbies()
        {
            throw new NotImplementedException();
        }
    }
}
