using Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace DinnergeddonService
{
    [ServiceContract]
    interface ILobbyService
    {
        /// <summary>
        /// Creates a public lobby
        /// </summary>
        /// <param name="name">The name of the lobby</param>
        /// <param name="playerLimit">The player limit of the lobby</param>
        /// <returns>The newly created lobby</returns>
        [OperationContract]
        Lobby CreateLobby(string name, int playerLimit);

        /// <summary>
        /// Creates a private lobby
        /// </summary>
        /// <param name="name">The name of the lobby</param>
        /// <param name="playerLimit">The player limit of the lobby</param>
        /// <param name="password">The password of the lobby</param>
        /// <returns>The newly created lobby</returns>
        [OperationContract]
        Lobby CreatePrivateLobby(string name, int playerLimit, string password);

        /// <summary>
        /// Gets all lobbies
        /// </summary>
        /// <returns>A list of all lobbies</returns>
        [OperationContract]
        IEnumerable<Lobby> GetLobbies();

        /// <summary>
        /// Tries to join an account to a lobby
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <param name="lobbyId">The ID of the lobby</param>
        /// <returns>If the account has joined successfully</returns>
        [OperationContract]
        bool JoinLobby(Guid accountId, Guid lobbyId);

        /// <summary>
        /// Adds a user to a private lobby
        /// </summary>
        /// <param name="accountId">The account ID</param>
        /// <param name="lobbyId">The lobby ID</param>
        /// <param name="password">The password of the lobby</param>
        /// <returns></returns>
        [OperationContract]
        bool JoinPrivateLobby(Guid accountId, Guid lobbyId, string password);

        /// <summary>
        /// Removes an account from a lobby
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <param name="lobbyId">The ID of the lobby</param>
        [OperationContract]
        void LeaveLobby(Guid accountId, Guid lobbyId);

        [OperationContract]
        Lobby GetLobbyById(Guid lobbyId);
    }
}
