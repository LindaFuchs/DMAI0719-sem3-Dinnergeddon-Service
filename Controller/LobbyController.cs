using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class LobbyController : ILobbyController
    {
        // A lobby container
        private readonly ILobbyContainer lobbyContainer;
        private readonly IAccountController accountController;
        // The minimum amount of players in a lobby
        private readonly int MIN_PLAYERS = 2;
        // The maximum amount of players in a lobby
        private readonly int MAX_PLAYERS = 4;

        public LobbyController(ILobbyContainer container, IAccountController accountController)
        {
            lobbyContainer = container;
            this.accountController = accountController;
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

            lobbyContainer.Add(lobby);
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
            return lobbyContainer.GetLobbies();
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

        /// <summary>
        /// Removes an account from a lobby
        /// TODO: IMPORTANT - make sure to update this method if needed once the group decides on
        /// how to solve the ownership of the lobby and what happens when they leave the lobby
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <param name="lobbyId">The ID of the lobby</param>
        public void LeaveLobby(Guid accountId, Guid lobbyId)
        {
            Lobby lobby = GetLobbyById(lobbyId);

            if (lobby == null)
                return;

            lock (lobby)
            {
                Account account = lobby.Players.Find((a) => a.Id == accountId);
                lobby.Players.Remove(account);

                if (lobby.Players.Count == 0)
                    lobbyContainer.Remove(lobby);
            }
        }

        public bool JoinLobby(Guid accountId, Guid lobbyId, string password)
        {
            Account account = accountController.FindById(accountId);
            Lobby lobby = GetLobbyById(lobbyId);

            // Check if both account and lobby actually exist
            if (account == null || lobby == null)
                return false;

            // Check if the account isn't already in another lobby
            foreach(Lobby l in GetLobbies())
                foreach(Account player in l.Players)
                    if (player.Equals(account))
                        return false;

            // If lobby is password protected
            if (lobby.PasswordHash != null && lobby.PasswordHash != "" && lobby.PasswordHash != string.Empty)
            {
                // If no password was passed to the method
                if (password == "" || password == null || password == string.Empty)
                {
                    return false;
                }
                return JoinLobby(account, lobby, password);
            }
            return JoinLobby(account, lobby);
        }
        
        private bool JoinLobby(Account account, Lobby lobby)
        {
            // Lock the lobby, prevent dirty reads
            lock (lobby)
            {
                // Check if the lobby can accept another account
                if (lobby.Players.Count >= lobby.Limit)
                    return false;

                lobby.Players.Add(account);
            }
            return true;
        }
        
        private bool JoinLobby(Account account, Lobby lobby, string password)
        {
            // Check the password
            bool pwCorrect = PasswordHasher.VerifyPassword(lobby.PasswordHash, password);

            if (!pwCorrect)
                return false;

            // If password is correct, proceed to normal lobby joining
            return JoinLobby(account, lobby);
        }

        /// <summary>
        /// Gets a lobby given an ID
        /// </summary>
        /// <param name="id">the ID of the lobby</param>
        /// <returns></returns>
        public Lobby GetLobbyById(Guid id)
        {
            return lobbyContainer.GetLobbyById(id);
        }
    }
}
