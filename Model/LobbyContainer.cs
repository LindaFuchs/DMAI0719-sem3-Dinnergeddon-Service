using System;
using System.Collections.Generic;

namespace Model
{
    public class LobbyContainer : ILobbyContainer
    {
        // Singleton
        private List<Lobby> lobbies;
        private List<Guid> accountsInLobbies;
        private static LobbyContainer instance;

        private LobbyContainer()
        {
            lobbies = new List<Lobby>();
            accountsInLobbies = new List<Guid>();
        }

        public static LobbyContainer GetInstance()
        {
            if (instance == null)
                instance = new LobbyContainer();
            return instance;
        }

        /// <summary>
        /// Gets all active lobbies
        /// </summary>
        /// <returns>A list of all active lobbies</returns>
        public IEnumerable<Lobby> GetLobbies()
        {
            return lobbies;
        }

        /// <summary>
        /// Adds a new lobby to the active lobbies
        /// </summary>
        /// <param name="lobby">The lobby object to be added</param>
        public void Add(Lobby lobby)
        {
            lobbies.Add(lobby);
        }

        /// <summary>
        /// Gets a lobby given an ID
        /// </summary>
        /// <param name="id">The ID of the lobby</param>
        /// <returns>The lobby that was found or null</returns>
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
