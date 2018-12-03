using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Removes a lobby from the list given an ID
        /// </summary>
        /// <param name="id">The ID of the lobby</param>
        public void Remove(Guid id)
        {
            // Finds an element using the ID passed
            Lobby lobby = lobbies.SingleOrDefault(l => l.Id == id);

            // If a lobby with that ID was found, it removes it
            if (lobby != null)
                lobbies.Remove(lobby);
        }

        /// <summary>
        /// Removes a lobby from the list
        /// </summary>
        /// <param name="lobby">A lobby object to remove</param>
        public void Remove(Lobby lobby)
        {
            Remove(lobby.Id);
        }
    }
}
