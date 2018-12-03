using System;
using System.Collections.Generic;

namespace Model
{
    public interface ILobbyContainer
    {
        /// <summary>
        /// Gets all lobbies
        /// </summary>
        /// <returns>A list of all lobbies</returns>
        IEnumerable<Lobby> GetLobbies();

        /// <summary>
        /// Adds a new active lobby
        /// </summary>
        /// <param name="lobby">The lobby to be added</param>
        void Add(Lobby lobby);

        /// <summary>
        /// Gets a lobby given an ID
        /// </summary>
        /// <param name="id">The ID of the lobby</param>
        /// <returns>The lobby found or null</returns>
        Lobby GetLobbyById(Guid id);

        /// <summary>
        /// Removes a lobby given an ID
        /// </summary>
        /// <param name="id">The ID of the lobby</param>
        void Remove(Guid id);

        /// <summary>
        /// Removes a lobby using the passed lobby's ID
        /// </summary>
        /// <param name="lobby">A lobby object with ID</param>
        void Remove(Lobby lobby);
    }
}
