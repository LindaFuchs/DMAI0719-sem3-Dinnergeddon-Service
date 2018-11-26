using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class LobbyController : ILobbyController
    {
        // A lobby container
        private readonly ILobbyContainer container;
        // The minimum amount of players in a lobby
        private readonly int MIN_PLAYERS = 2;
        // The maximum amount of players in a lobby
        private readonly int MAX_PLAYERS = 4;

        public LobbyController(ILobbyContainer container)
        {
            this.container = container;
        }
        
        /// <summary>
        /// Creates a lobby object and stores it on the system.
        /// The lobby created by this method will be a non-private one, so anyone will
        /// be able to join
        /// </summary>
        /// <param name="name">The name of the lobby</param>
        /// <param name="playerLimit">The player limit(minimum of 2, maximum of 4)</param>
        /// <returns>The created lobby object</returns>
        public Lobby CreateLobby(string name, int playerLimit)
        {
            if (!CheckLimits(name, playerLimit))
                return null;

            Lobby lobby = BuildLobby(name, playerLimit, "");

            container.Add(lobby);
            return lobby;
        }

        /// <summary>
        /// Creates a lobby object and stores it on the system
        /// The lobby created by this method will be a private one and only players with
        /// the password will be able to join
        /// </summary>
        /// <param name="name">The name of the lobby</param>
        /// <param name="playerLimit">The player limit(minimum of 2, maximum of 4)</param>
        /// <param name="password">The password that's protecting the lobby</param>
        /// <returns>The created lobby object</returns>
        public Lobby CreateLobby(string name, int playerLimit, string password)
        {
            Lobby lobby = CreateLobby(name, playerLimit);
            if (lobby == null)
                return null;
            
            string passwordHash = PasswordHasher.HashPassword(password);

            lobby.PasswordHash = passwordHash;
            return lobby;
        }

        /// <summary>
        /// Gets all the currently active lobbies
        /// </summary>
        /// <returns>A list of all currently active lobbies</returns>
        public IEnumerable<Lobby> GetLobbies()
        {
            return container.GetLobbies();
        }

        /// <summary>
        /// This method checks if the limits of Lobby creation are met
        /// </summary>
        /// <param name="name">The name of the lobby</param>
        /// <param name="playerLimit">The player limit of the lobby</param>
        /// <returns>If the lobby can be created</returns>
        private bool CheckLimits(string name, int playerLimit)
        {
            if (name == "" || playerLimit < MIN_PLAYERS || playerLimit > MAX_PLAYERS)
                return false;
            return true;
        }

        /// <summary>
        /// Builds a new Lobby object
        /// </summary>
        /// <param name="name">The name of the lobby</param>
        /// <param name="limit">The limit of players</param>
        /// <param name="passwordHash">The password hash</param>
        /// <returns>A new lobby object with those parameters</returns>
        private Lobby BuildLobby(string name, int limit, string passwordHash)
        {
            if (name == "" || name == null)
                throw new ArgumentNullException("Name cannot be empty or null");

            return new Lobby()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Limit = limit,
                Players = new List<Account>(),
                PasswordHash = passwordHash,
            };
        }
    }
}
