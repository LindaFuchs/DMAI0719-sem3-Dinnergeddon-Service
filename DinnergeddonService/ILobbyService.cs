using Model;
using System.ServiceModel;
using System.Collections.Generic;

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
    }
}
