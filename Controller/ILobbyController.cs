using Model;
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
    }
}
