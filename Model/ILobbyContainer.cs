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
    }
}
