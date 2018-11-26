using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public interface ILobbyController
    {
        /// <summary>
        /// Creates a new lobby
        /// </summary>
        /// <param name="name">The name of the lobby</param>
        /// <param name="playerLimit">The maximum player limit of the lobby</param>
        /// <returns>The newly created lobby</returns>
        Lobby CreateLobby(string name, int playerLimit);

        /// <summary>
        /// Creates a new private lobby
        /// </summary>
        /// <param name="name">The name of the lobby</param>
        /// <param name="playerLimit">The maximum player limit</param>
        /// <param name="password">The password of the lobby</param>
        /// <returns>The newly created lobby</returns>
        Lobby CreateLobby(string name, int playerLimit, string password);

        /// <summary>
        /// Gets all the lobbies currently in the system
        /// </summary>
        /// <returns>A list of all the lobbies currenlty in the system</returns>
        IEnumerable<Lobby> GetLobbies();

        /// <summary>
        /// Adds an account to a lobby
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <param name="lobbyId">The ID of the lobby</param>
        /// <returns>If the operation was successful</returns>
        bool JoinLobby(Guid accountId, Guid lobbyId);

        /// <summary>
        /// Adds an account to a private lobby
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <param name="lobbyId">The ID of the lobby</param>
        /// <param name="password">The password of the lobby</param>
        /// <returns>If the operation was successful</returns>
        bool JoinLobby(Guid accountId, Guid lobbyId, string password);

        /// <summary>
        /// Removes an account from a lobby
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <param name="lobbyId">The ID of the lobby</param>
        void LeaveLobby(Guid accountId, Guid lobbyId);

        /// <summary>
        /// Gets a lobby given an ID
        /// </summary>
        /// <param name="id">The ID of the lobby</param>
        /// <returns>A lobby with ID or null</returns>
        Lobby GetLobbyById(Guid id);
    }
}
