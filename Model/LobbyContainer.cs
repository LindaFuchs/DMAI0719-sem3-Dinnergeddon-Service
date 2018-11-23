using System.Collections.Generic;

namespace Model
{
    public class LobbyContainer : ILobbyContainer
    {
        private List<Lobby> lobbies;
        private static LobbyContainer instance;

        private LobbyContainer()
        {
            lobbies = new List<Lobby>();
        }

        public static LobbyContainer GetInstance()
        {
            if (instance == null)
                instance = new LobbyContainer();
            return instance;
        }

        public IEnumerable<Lobby> GetActiveLobbies()
        {
            return lobbies;
        }

        public void Add(Lobby lobby)
        {
            lobbies.Add(lobby);
        }
    }
}
