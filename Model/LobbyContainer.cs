using System;
using System.Collections.Generic;

namespace Model
{
    public class LobbyContainer : ILobbyContainer
    {
        // Singleton
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

        /// <summary>
        /// TODO:
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Lobby> GetLobbies()
        {
            return lobbies;
        }

        /// <summary>
        /// TODO:
        /// </summary>
        /// <param name="lobby"></param>
        public void Add(Lobby lobby)
        {
            lobbies.Add(lobby);
        }

        /// <summary>
        /// TODO:
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Lobby GetLobbyById(Guid id)
        {
            foreach (Lobby l in lobbies)
            {
                if (l.Id == id)
                    return l;
            }
            return null;
        }
    }
}
